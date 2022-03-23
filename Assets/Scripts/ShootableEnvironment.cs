using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script extends from ShootableEntity, and is for any shootable environment objects.
//The difference is that shootable environment objects will automatically reduce their time status after 5 seconds.
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

    protected override void gotSlowed()
    {
        base.gotSlowed();
        if(routine!= null) StopCoroutine(routine);
        reducing = false;
    }

    protected override void gotSpeed()
    {
        base.gotSpeed();
        if (routine != null) StopCoroutine(routine);
        reducing = false;
    }
}
