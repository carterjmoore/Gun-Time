using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : ShootableEntity
{
    public float speed = 8f;
    public GameObject player;

    Rigidbody rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
    }

}
