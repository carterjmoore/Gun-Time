using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneController : MonoBehaviour
{
    public AudioSource LaserNoise;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PhysEnemy")) {
            LaserNoise.Play();

            Destroy(other.gameObject);
        }

        //Allow to fire through other enemies
        else if (other.gameObject.CompareTag("Kill"))
        {
            LaserNoise.Play();

            Destroy(other.gameObject);
        }
    }
}
