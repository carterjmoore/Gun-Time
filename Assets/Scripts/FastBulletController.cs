using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBulletController : MonoBehaviour
{

    public float speed = 1f;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (velocity != null)
        {
            transform.Translate(velocity * Time.deltaTime * speed);
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


    

}
