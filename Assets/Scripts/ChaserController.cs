using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{
    public float speed = 8f;
    public GameObject player;

    Rigidbody rb;
    Vector3 startPos;
    Vector3 targetPos;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        Debug.Log(playerVisible());
        if (playerVisible())
        {
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
        Vector3 movementDir = new Vector3(targetDir.x, 0f, targetDir.z);
        rb.velocity = movementDir * speed;
    }

    //Check if player is visible to the enemy
    bool playerVisible()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0f, 0.5f, 0f), player.transform.position, Color.green);
        if (Physics.Raycast(transform.position, player.transform.position, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }
}
