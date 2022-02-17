﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEntity : MonoBehaviour
{
    //Represents current slow/speed status. Negative means slowed, positive means sped up
    protected int timeStatus;
    //The max magnitude for timeStatus (positive or negative)
    int maxStatus;

    protected virtual void Start()
    {
        timeStatus = 0;
        maxStatus = 3;
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
        if (other.gameObject.CompareTag("Slow"))
        {
            gotSlowed();
        }
        if (other.gameObject.CompareTag("Speed"))
        {
            gotSpeed();
        }
    }

    protected float timeMultiplier()
    {
        //If maximum slow value, entity is stopped
        if (timeStatus == -maxStatus) return 0;

        return Mathf.Pow(2, timeStatus);
    }

    void gotSlowed()
    {
        timeStatus = Mathf.Max(timeStatus - 1, -maxStatus);
    }

    void gotSpeed()
    {
        timeStatus = Mathf.Min(timeStatus + 1, maxStatus);
    }
}