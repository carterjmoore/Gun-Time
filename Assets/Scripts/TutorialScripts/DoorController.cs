using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pos2 can be defined in the unity editor, and pos1 is where the platform is on Start()
public class DoorController : ShootableEnvironment
{

    [Header("Door Options")]
    public bool isSimpleDoor; //A Simple Door is a door that simply opens after OpenDoor() is called once

    [Header("Door Controls")]
    public float speed = 6f;
    public float lerpDistance = 5f;
    public float waitTime = 2f;

    Vector3 pos1;
    Vector3 pos2;

    float distance;
    float fraction;
    bool waitingToGoForward;
    bool waitingToGoBack;
    bool movingBack;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pos1 = transform.position;
        pos2 = pos1 + transform.up * lerpDistance;
        distance = Vector3.Distance(pos1, pos2);
        movingBack = false;
        waitingToGoBack = false;
        waitingToGoForward = false;
        fraction = 0;
    }

    //Lerp between two positions, but pause upon arriving at destination
    void FixedUpdate()
    {
        if (!isSimpleDoor)
        {
            if (!movingBack && !waitingToGoForward)
            {
                fraction += speed / distance * timeMultiplier() * Time.deltaTime;
                fraction = Mathf.Clamp01(fraction);
                transform.position = Vector3.Lerp(pos1, pos2, fraction);
                if (fraction == 1)
                {
                    waitingToGoBack = true;
                    movingBack = true;
                }
            }
            else if (!waitingToGoBack)
            {
                fraction -= speed / distance * timeMultiplier() * Time.deltaTime;
                fraction = Mathf.Clamp01(fraction);
                transform.position = Vector3.Lerp(pos1, pos2, fraction);
                if (fraction == 0)
                {
                    StartCoroutine(waitToGoForward());
                    movingBack = false;
                }
            }
        }
        else
        {
            if (!waitingToGoForward)
            {
                fraction += speed / distance * timeMultiplier() * Time.deltaTime;
                fraction = Mathf.Clamp01(fraction);
                transform.position = Vector3.Lerp(pos1, pos2, fraction);
                if (fraction == 1)
                {
                    waitingToGoBack = true;
                }
            }
            else if (!waitingToGoBack)
            {
                fraction -= speed / distance * timeMultiplier() * Time.deltaTime;
                fraction = Mathf.Clamp01(fraction);
                transform.position = Vector3.Lerp(pos1, pos2, fraction);
            }
        }
    }

    IEnumerator waitToGoForward()
    {
        waitingToGoForward = true;
        yield return new WaitForSecondsRealtime(waitTime);
        waitingToGoForward = false;
    }

    public float getTimeMultiplier()
    {
        return this.timeMultiplier();
    }

    public void OpenDoor() //If the door is closed, then Open the Door, otherwise keep it closed
    {
        if (waitingToGoBack)
        {
            waitingToGoBack = false;
        }
    }
    public void CloseDoor() //If the door is open, then Close the Door, otherwise keep it open
    {
        if (waitingToGoForward && isSimpleDoor)
        {
            waitingToGoForward = false;
        }
    }
    public bool CheckDoorOpen() //Accessor Method for waitingToGoBack
    {
        return waitingToGoBack;
    }
}
