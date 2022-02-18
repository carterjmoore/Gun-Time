using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{
    public float speed = 8f;
    public GameObject player;

    Rigidbody rb;
    Vector3 targetPos;
    float distanceTolerance;

    bool reachedTarget;
    bool haveAdjustedTarget;

    protected override void Start()
    {
        base.Start();
        rb = transform.GetComponent<Rigidbody>();
        targetPos = transform.position;
        distanceTolerance = 0.1f;
        reachedTarget = true;
        haveAdjustedTarget = true;
    }

    void FixedUpdate()
    {
        //If player is visible, look at player and set targetPos to player's pos
        if (playerVisible())
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.y));
            targetPos = player.transform.position;
            haveAdjustedTarget = false;
        }
        //If just lost sight of player, set targetPos to slightly ahead of its current position, to make sure chaser gets around corner
        else if (!haveAdjustedTarget)
        {
            targetPos += getTargetDir() * 0.5f;
            haveAdjustedTarget = true;
        }
        moveToTarget();
    }

    void moveToTarget()
    {
        //If hasn't yet reached target, and is within the tolerance, snap to target
        if (distanceTolerance * distanceTolerance > Vector3.Distance(transform.position, targetPos) && !reachedTarget)
        {
            transform.position = targetPos;
            reachedTarget = true;
        }
        //If not within tolerance, set velocity towards target
        else
        {
            reachedTarget = false;
            rb.velocity = new Vector3(getTargetDir().x * speed * timeMultiplier(), rb.velocity.y, getTargetDir().z * speed * timeMultiplier());
        }
    }

    //Check if player is visible to the enemy
    bool playerVisible()
    {
        //Raycast from chaser to player to check if player is visible
        RaycastHit hit;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity))
        {
            //If raycast hits player before anything else, player is visible
            if(hit.transform.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 getTargetDir()
    {
        return (targetPos - transform.position).normalized;
    }
}
