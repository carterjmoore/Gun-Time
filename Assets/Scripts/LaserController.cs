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
            //LaserNoise.Play();
            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
            AudioSource.PlayClipAtPoint(LaserNoise.clip, other.transform.position);
        }

        //Allow to fire through other enemies
        else if (other.gameObject.CompareTag("PhysEnemy"))
        {
            //LaserNoise.Play();
            AudioSource.PlayClipAtPoint(LaserNoise.clip, other.transform.position);
            Destroy(other.gameObject);
        }
    }
}
