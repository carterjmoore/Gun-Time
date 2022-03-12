using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This handles time status for anything that can be shot by the player
public class ShootableEntity : MonoBehaviour
{
    //Represents current slow/speed status. Negative means slowed, positive means sped up
    protected int timeStatus;
    //The max magnitude for timeStatus (positive or negative)
    int maxStatus;

    bool timeJustChanged;

    protected virtual void Start()
    {
        timeStatus = 0;
        maxStatus = 3;
        timeJustChanged = false;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gotSlowed();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            gotSpeed();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Slow") && !timeJustChanged)
        {
            StartCoroutine(TimeJustChanged());
            Destroy(other.gameObject);
            gotSlowed();
        }
        if (other.gameObject.CompareTag("Speed") && !timeJustChanged)
        {
            StartCoroutine(TimeJustChanged());
            Destroy(other.gameObject);
            gotSpeed();
        }
    }

    protected float timeMultiplier()
    {
        //If maximum slow value, entity is stopped
        if (timeStatus == -maxStatus) return 0;

        return Mathf.Pow(2, timeStatus);
    }

    protected virtual void gotSlowed()
    {
        timeStatus = Mathf.Max(timeStatus - 1, -maxStatus);
    }

    protected virtual void gotSpeed()
    {
        timeStatus = Mathf.Min(timeStatus + 1, maxStatus);
    }

    //For some reason, time status was getting applied twice sometimes, so this is a bandaid to fix it
    //It seemed as though the OnTriggerEnter() was getting triggered twice by the projectile, but I couldn't figure out why
    IEnumerator TimeJustChanged()
    {
        timeJustChanged = true;
        yield return new WaitForEndOfFrame();
        timeJustChanged = false;
    }
}
