using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pos2 can be defined in the unity editor, and pos1 is where the platform is on Start()
public class LauncherController : ShootableEnvironment
{
    [Header("Launcher Controls")]
    public float speed = 6f;
    public float lerpDistance = 5f;
    public float launchForce = 10f;
    public float waitTime = 3f;
    [Header("Other Options")]
    [SerializeField] bool isVertical = true; //Turn on if launcher goes straight up and down
    [SerializeField] bool alwaysLaunch = false; //By default, we only launch when timeMultiplier > 1

    Vector3 pos1;
    Vector3 pos2;

    float distance;
    float fraction;
    bool movingBack;
    bool waitingToGoBack;
    bool waitingToGoForward;
    bool launch;
    bool walkOffLaunchDelay;
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
        launch = false;
        walkOffLaunchDelay = false;
        fraction = 0;
    }

    //Lerp between two positions, but pause upon arriving at destination
    void FixedUpdate()
    {
        if (!movingBack && !waitingToGoForward)
        {
            fraction += speed / distance * timeMultiplier() * Time.deltaTime;
            fraction = Mathf.Clamp01(fraction);
            transform.position = Vector3.Lerp(pos1, pos2, fraction);
            if (fraction == 1)
            {
                StartCoroutine(waitToGoBack());
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

    IEnumerator waitToGoBack()
    {
        waitingToGoBack = true;

        //At the instant the top position is reached, enable launch
        launch = true;
        yield return new WaitForSecondsRealtime(0.01f);
        launch = false;


        yield return new WaitForSecondsRealtime(waitTime / timeMultiplier());
        waitingToGoBack = false;
    }

    IEnumerator waitToGoForward()
    {
        waitingToGoForward = true;
        yield return new WaitForSecondsRealtime(waitTime / timeMultiplier());
        waitingToGoForward = false;
    }

    //Make sure OnTriggerExit launch doesn't get appplied multiple times at once
    IEnumerator delayWalkOffLaunch()
    {
        walkOffLaunchDelay = true;
        yield return new WaitForSecondsRealtime(0.1f);
        walkOffLaunchDelay = false;
    }

    //Launch the player if they leave the launcher while it's going up
    private void OnTriggerExit(Collider other)
    {
        //Instead of comparing tag with player, this should 
        if (!movingBack && !waitingToGoForward && fastEnoughToLaunch() && !launch && !walkOffLaunchDelay && other.attachedRigidbody)
        {
            StartCoroutine(delayWalkOffLaunch());
            Launch(other);
        }
        other.transform.parent = null;
    }

    //Launch the player if they're on the launcher when it reaches the top
    private void OnTriggerStay(Collider other)
    {
        if (launch && fastEnoughToLaunch() && other.attachedRigidbody)
        {
            Launch(other);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.attachedRigidbody && isVertical)
        {
            other.transform.parent = transform;
        }
    }

    //Laucnch player in direction of transform.up
    void Launch(Collider other)
    {
        other.transform.parent = null;
        Rigidbody rb = other.attachedRigidbody;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);
        other.attachedRigidbody.AddForce(transform.up * timeMultiplier() * launchForce, ForceMode.Impulse);
        launch = false;
    }

    bool fastEnoughToLaunch()
    {
        return alwaysLaunch || timeMultiplier() > 1;
    }
}
