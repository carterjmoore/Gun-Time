using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        if (velocity != null)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }


    //This function will initialize the projectile's position and velocity
    //Inputs: pos: position of instantiation, vel: intial velocity of projectile
    //Outputs: none
    //Notes: The projectile has a constant speed, but we need a velocity vector for direction too
    public void InitProjectile(Vector3 pos, Vector3 vel)
    {
        transform.position = pos;
        velocity = vel;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}