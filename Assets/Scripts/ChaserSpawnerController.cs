using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSpawnerController : MonoBehaviour
{
    public bool spawnRepeatedly = false;
    public float spawnTime = 5f;
    public GameObject chaser;

    bool spawnNow;
    GameObject playerReference;

    private void Start()
    {
        spawnNow = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNow)
        {
            spawn();
            StartCoroutine(waitForSpawn());
        }
    }

    void spawn()
    {
        GameObject c = Instantiate(chaser, transform.position, Quaternion.identity);
        c.GetComponent<ChaserController>().setReferences(playerReference);
    }

    public void spawnTriggered(GameObject player)
    {
        playerReference = player;
        if (spawnRepeatedly) spawnNow = true;
        else spawn();
    }

    IEnumerator waitForSpawn()
    {
        spawnNow = false;
        yield return new WaitForSecondsRealtime(spawnTime);
        spawnNow = true;
    }
}
