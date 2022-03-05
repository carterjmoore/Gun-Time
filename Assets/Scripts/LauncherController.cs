using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pos2 can be defined in the unity editor, and pos1 is where the platform is on Start()
public class LauncherController : ShootableEnvironment
{
    public float speed = 6f;
    public float launchForce = 10f;

    Vector3 pos1;
    public Vector3 pos2;

    float distance;
    float fraction;
    bool movingBack;
    bool waitingToGoBack;
    bool waitingToGoForward;
    bool launch;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pos1 = transform.position;
        distance = Vector3.Distance(pos1, pos2);
        movingBack = false;
        waitingToGoBack = false;
        waitingToGoForward = false;
        launch = false;
        fraction = 0;
    }

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
        launch = true;
        yield return new WaitForSecondsRealtime(0.01f);
        launch = false;
        yield return new WaitForSecondsRealtime(3f / timeMultiplier());
        waitingToGoBack = false;
    }

    IEnumerator waitToGoForward()
    {
        waitingToGoForward = true;
        yield return new WaitForSecondsRealtime(3f / timeMultiplier());
        waitingToGoForward = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (launch && timeMultiplier() > 1)
        {
            Debug.Log("Launch");
            collision.transform.parent = null;
            collision.rigidbody.AddExplosionForce(timeMultiplier() * launchForce, collision.contacts[0].point, 5);
            launch = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!movingBack)
        {

        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
