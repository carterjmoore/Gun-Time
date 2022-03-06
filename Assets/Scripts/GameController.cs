using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public bool enableChasers = true;
    public bool enableShooters = true;
    public bool invincibility = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) enableChasers = !enableChasers;
        if (Input.GetKeyDown(KeyCode.G)) enableShooters = !enableShooters;
        if (Input.GetKeyDown(KeyCode.I)) invincibility = !invincibility;
    }

    public bool chasersEnabled()
    {
        return enableChasers;
    }

    public bool shootersEnabled()
    {
        return enableShooters;
    }

    public bool invincible()
    {
        return invincibility;
    }
}
