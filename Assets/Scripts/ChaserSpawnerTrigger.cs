using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSpawnerTrigger : MonoBehaviour
{
    public ChaserSpawnerController spawner;
    bool triggered;

    void Start()
    {
        triggered = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.CompareTag("Player"))
        {
            spawner.spawnTriggered(other.gameObject);
            triggered = true;
        }
    }
}