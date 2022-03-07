using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script taken from: https://www.youtube.com/watch?v=cTIAhwlvW9M
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    bool dead = false;
    Rigidbody rb;
    BoxCollider bc;
    float totalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
        rb.useGravity = false;
        totalRotation = 0f;
    }

    void Update()
    {
        if (!dead)
        {
            transform.position = cameraPosition.position;
        }
        else if(totalRotation < 90)
        {
            transform.Rotate(new Vector3(0f, 0f, 180f) * Time.deltaTime);
            totalRotation += 180 * Time.deltaTime;
        }
    }

    public void TriggerDeath()
    {
        dead = true;
        GameObject player = transform.parent.gameObject;
        transform.parent = null;
        bc.enabled = true;
        rb.useGravity = true;
        player.gameObject.SetActive(false);
    }
}
