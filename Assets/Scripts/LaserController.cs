using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().TriggerDeath();
        }

        //Allow to fire through other enemies
        else if(other.gameObject.CompareTag("PhysEnemy")) Destroy(other.gameObject);
    }
}
