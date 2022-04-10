using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerKillerController : MonoBehaviour
{
    public AudioSource LaserNoise;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LaserNoise.Play();

            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
        }
    }
}
