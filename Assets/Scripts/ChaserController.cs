using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{
    public float speed = 8f;
    public GameObject player;

    Rigidbody rb;
    Vector3 targetPos;
    Vector3 playerPosRightAfterLostLOS; //Store the player pos a fraction of a second after they leave LOS, to improve chasing
    float distanceTolerance;

    bool reachedTarget;
    bool haveAdjustedTarget;

    protected override void Start()
    {
        base.Start();
        rb = transform.GetComponent<Rigidbody>();
        targetPos = transform.position;
        playerPosRightAfterLostLOS = targetPos;
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
            targetPos += getTargetDir() * 1f;
            StartCoroutine(setPlayerPosRightAfterLostLOS());
            haveAdjustedTarget = true;
        }
        moveToTarget();
    }

    void moveToTarget()
    {
        //If within the tolerance
        if (checkIfWithinTolerance())
        {
            //If haven't reached target, snap to target
            if (!reachedTarget)
            {
                transform.position = targetPos;
            }

            Debug.Log("in here");
            //If just reached last seen player pos, now move toward pos of player slightly after lost LOS for improved chasing
            if(targetPos != playerPosRightAfterLostLOS)
            {
                targetPos = playerPosRightAfterLostLOS;
            }
            //Otherwise, stop chasing
            else
            {
                reachedTarget = true;
                Debug.Log("Trans: "+transform.position);
            }
        }
        //If not within tolerance, set velocity towards target
        else
        {
            Debug.Log(targetPos);
            Debug.Log(transform.position);
            Debug.Log(checkIfWithinTolerance());
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
        Debug.Log("Lost LOS: "+playerPosRightAfterLostLOS);
    }
}
