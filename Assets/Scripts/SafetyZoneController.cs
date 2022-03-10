using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PhysEnemy")) {
            Destroy(other.gameObject);
        }

        //Allow to fire through other enemies
        else if (other.gameObject.CompareTag("Kill"))
        {
            Destroy(other.gameObject);
        }
    }
}
