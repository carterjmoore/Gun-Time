using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatformController : ShootableEnvironment
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime* timeMultiplier());
    }
}
