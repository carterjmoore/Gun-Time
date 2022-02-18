using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{
    public float speed = 8f;
    public GameObject player;

    Rigidbody rb;
    Vector3 startPos;
    Vector3 lastSeenPlayerPos;
    Vector3 targetPos;

    protected override void Start()
    {
        base.Start();
        rb = transform.GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        Debug.Log(playerVisible());
        if (playerVisible())
        {
            //transform.LookAt(player.transform);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.y));
            targetPos = player.transform.position;
            moveToTarget();
        }
        else if (targetPos != null)
        {

        }
    }

    void moveToTarget()
    {
        Vector3 targetDir = (targetPos - transform.position).normalized;
        rb.velocity = new Vector3(targetDir.x * speed * timeMultiplier(), rb.velocity.y, targetDir.z * speed * timeMultiplier());
    }

    //Check if player is visible to the enemy
    bool playerVisible()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }
}
