using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public AudioSource LaserNoise;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LaserNoise.Play();
            other.gameObject.GetComponent<PlayerController>().TriggerDeath();

        }

        //Allow to fire through other enemies
        else if (other.gameObject.CompareTag("PhysEnemy"))
        {
            LaserNoise.Play();
            Destroy(other.gameObject);
        }
    }
}
