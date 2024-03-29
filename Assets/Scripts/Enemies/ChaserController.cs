﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{

    [Header("Movement")]
    public float speed = 5.9f;
    public float movementMultiplier = 10f;
    public float airMovementMultiplier = 0f;
    public float groundDrag = 6f;
    public float airDrag = 1.5f;

    [Header("Chase Options")]
    public bool chaseThroughLasers = true;
    public LayerMask ignoreMask;

    [Header("References")]
    public GameObject player;
    public GameController gameController;

    float chaserHeight = 1f;
    bool isGrounded;

    PlayerController playerController;
    Rigidbody rb;
    Vector3 targetPos;
    Vector3 playerPosRightAfterLostLOS; //Store the player pos a fraction of a second after they leave LOS, to improve chasing
    float distanceTolerance;
    RaycastHit slopeHit;

    bool reachedTarget;
    bool haveAdjustedTarget;
    public AudioSource AttackSound;



    protected override void Start()
    {
        base.Start();
        AttackSound = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        targetPos = transform.position;
        playerPosRightAfterLostLOS = targetPos;
        distanceTolerance = 0.1f;
        reachedTarget = true;
        haveAdjustedTarget = true;

        //If we want to chase through lasers, remove laser from ignoreMask
        if (!chaseThroughLasers) ignoreMask = LayerMask.GetMask("TransparentFX");
    }

    protected override void Update()
    {
        base.Update();

        //Find new target
        updateTarget();

        //Make sure chasers don't look at player or update player last seen position when frozen
        //Also forget last seen position when frozen
        if (timeMultiplier() == 0 || !gameController.chasersEnabled())
        {
            targetPos = transform.position;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            return;
        }
        //Look up ramp if on slope
        else if (onSlope())
        {
            Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(targetPos - transform.position, slopeHit.normal), Color.red);
            transform.LookAt(Vector3.ProjectOnPlane(targetPos - transform.position, slopeHit.normal) + new Vector3(0f, 1f, 0f));
            rb.constraints = RigidbodyConstraints.None;
        }
        //Otherwise look in direction of player, but parallel to ground
        else
        {
            Debug.DrawRay(transform.position, targetPos - transform.position, Color.red);
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationX;
        }
    }

    void FixedUpdate()
    {
        //Make sure chasers don't update anything or move around walls if frozen
        if (timeMultiplier() == 0 || !gameController.chasersEnabled())
        {
            return;
        }

        //If player is dead, do nothing
        if (playerController.IsDead()) return;

        isGrounded = Physics.CheckBox(transform.position - new Vector3(0, 0.4f, 0), new Vector3(0.6f, 0.2f, 0.6f), transform.rotation, ~ignoreMask);
        setDrag();

        //Find new target
        updateTarget();

        //Avoid walls
        moveAroundWalls();

        //If player is not dead, move to target
        if (!playerController.IsDead())
        {
            moveToTarget();
        }
    }

    void updateTarget()
    {
        //If player is visible, look at player and set targetPos to player's pos
        if (playerVisible())
        {
            targetPos = player.transform.position;
            haveAdjustedTarget = false;
        }
        //If just lost sight of player, set targetPos to slightly ahead of its current position, to make sure chaser gets around corner
        else if (!haveAdjustedTarget)
        {
            targetPos += getTargetDir() * 1f;
            StartCoroutine(setPlayerPosRightAfterLostLOS());
            haveAdjustedTarget = true;
        }
    }

    void moveToTarget()
    {
        //If within the tolerance
        if (checkIfWithinTolerance())
        {
            //If haven't reached target, snap to target
            if (!reachedTarget && !playerVisible())
            {
                transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            }

            //If just reached last seen player pos, now move toward pos of player slightly after lost LOS for improved chasing
            if(targetPos != playerPosRightAfterLostLOS)
            {
                targetPos = playerPosRightAfterLostLOS;
            }
            //Otherwise, stop chasing
            else
            {
                reachedTarget = true;
            }
        }
        //If not within tolerance, set velocity towards target
        else
        {
            reachedTarget = false;

            //If on slope, add force along slope. Otherwise, add force toward target
            Vector3 moveDirection = onSlope() ? Vector3.ProjectOnPlane(targetPos-transform.position, slopeHit.normal).normalized : getTargetDir();
            float multiplier = isGrounded ? movementMultiplier : movementMultiplier*airMovementMultiplier;
            rb.AddForce(speed * multiplier * moveDirection * timeMultiplier(), ForceMode.Acceleration);
        }
    }

    void moveAroundWalls()
    {
        Vector3 right = Vector3.Cross(transform.forward, transform.up).normalized;
        Vector3 left = -right;

        //If there is a wall diagonally to the right, avoid it
        if (Physics.Raycast(transform.position, transform.forward + right, 1.5f, ~ignoreMask))
        {
            rb.AddForce(left * speed * movementMultiplier / 2, ForceMode.Acceleration);
        }
        //If the is a wall diagonally to the left, avoid it
        else if (Physics.Raycast(transform.position, transform.forward + left, 1.5f, ~ignoreMask))
        {
            rb.AddForce(right * speed * movementMultiplier / 2, ForceMode.Acceleration);
        }
    }

    //Check if player is visible to the enemy
    bool playerVisible()
    {
        //Raycast from chaser to player to check if player is visible
        RaycastHit hit;

        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity, ~ignoreMask)){
            //If raycast hits player before anything else, player is visible
            if (hit.transform.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 getTargetDir()
    {
        Vector3 targetDir = (targetPos - transform.position);
        targetDir.y = 0f; //We only want movement parallel to the ground
        return targetDir.normalized;
    }

    bool checkIfWithinTolerance()
    {
        //Ignore Y coordinate for distance checking
        Vector2 thisPosNoY = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosNoY = new Vector2(targetPos.x, targetPos.z);
        return distanceTolerance > Vector2.Distance(thisPosNoY, targetPosNoY);
    }

    IEnumerator setPlayerPosRightAfterLostLOS()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        playerPosRightAfterLostLOS = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            AttackSound.Play();
            playerController.TriggerDeath();
        }
    }

    //Check if the chaser is on a slope
    bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, chaserHeight / 2 + 0.55f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }

        //Also do a check if there is a slope in front. Since chaser is a cube, it struggles to initially get onto the slope
        RaycastHit forwardHitCheck;
        if (Physics.Raycast(transform.position - new Vector3(0f, chaserHeight/2+0.1f, 0f), transform.forward, out forwardHitCheck, 1.5f))
        {
            if(Vector3.Dot(forwardHitCheck.normal, Vector3.up) != 0)
            {
                return true;
            }
        }
        return false;
    }

    void setDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    public void setReferences(GameObject playerObject)
    {
        player = playerObject;
        gameController = player.GetComponent<PlayerController>().gameController;
    }
}