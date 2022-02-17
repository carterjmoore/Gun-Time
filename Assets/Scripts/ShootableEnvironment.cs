using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEnvironment : ShootableEntity
{
    bool reducing;
    Coroutine routine;

    protected override void Start()
    {
        base.Start();
        reducing = false;
    }
    protected override void Update()
    {
        base.Update();
        Debug.Log(timeStatus);
        Debug.Log(reducing);
        if (timeStatus != 0 && !reducing)
        {
            //Start coroutine for reducing time status
            routine = StartCoroutine(ReduceTimeStatusAgain());
        }
    }

    IEnumerator ReduceTimeStatusAgain()
    {
        reducing = true;
        yield return new WaitForSecondsRealtime(5);
        if(timeStatus != 0)
        {
            //If timestatus is positive, reduce it by one, if it is negative, increase it by one
            timeStatus = (timeStatus > 0) ? --timeStatus : ++timeStatus;
        }
        reducing = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if ((other.gameObject.CompareTag("Slow") || other.gameObject.CompareTag("Speed")) && routine != null)
        {
            StopCoroutine(routine);
            reducing = false;
        }
    }
}
