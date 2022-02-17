using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : ShootableEnvironment
{
    public float speed = 6f;

    Vector3 pos1;
    Vector3 pos2;
    float distance;
    float fraction;
    bool movingBack;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pos1 = transform.position;
        pos2 = new Vector3(pos1.x + 20f, pos1.y, pos1.z);
        distance = Vector3.Distance(pos1, pos2);
        movingBack = false;
        fraction = 0;
    }

    void FixedUpdate()
    {
        if(!movingBack)
        {
            fraction += speed / distance * timeMultiplier() * Time.deltaTime;
            fraction = Mathf.Clamp01(fraction);
            transform.position = Vector3.Lerp(pos1, pos2, fraction);
            if (fraction == 1) movingBack = true;
        }
        else
        {
            fraction -= speed / distance * timeMultiplier() * Time.deltaTime;
            fraction = Mathf.Clamp01(fraction);
            transform.position = Vector3.Lerp(pos1, pos2, fraction);
            if (fraction == 0) movingBack = false;
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
