using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBulletController : MonoBehaviour
{
    public Vector3 velocity;
    public AudioSource gotShot;

    // Update is called once per frame
    void Update()
    {
        if (velocity != null)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }

    public void InitProjectile(Vector3 pos, Vector3 vel)
    {

        transform.position = pos;
        velocity = vel;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player")){
            gotShot.Play();

            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
        }
    }
}