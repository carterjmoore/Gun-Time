using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatformController : ShootableEnvironment
{
    public float knockbackForce = 2000;
    public float rotationSpeed = 90f;
    bool collidedWithSide;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collidedWithSide = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime* timeMultiplier());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.attachedRigidbody)
        {
            collidedWithSide = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collidedWithSide)
        {
            //This line was taken and modified from https://www.youtube.com/watch?time_continue=192&v=bdGn4J4HD_8&feature=emb_title
            collision.rigidbody.AddExplosionForce(knockbackForce * timeMultiplier(), collision.contacts[0].point, 5);

            collidedWithSide = false;
        }
    }
}
