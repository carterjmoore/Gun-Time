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
        reducing = true;
    }
    protected virtual void Update()
    {
        if(timeStatus != 0 && !reducing)
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
            timeStatus = timeStatus > 0 ? timeStatus-- : timeStatus++;
        }
        reducing = false;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if ((collision.gameObject.CompareTag("Slow") || collision.gameObject.CompareTag("Speed")) && routine != null)
        {
            StopCoroutine(routine);
            reducing = false;
        }
    }
}
