using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [Header("UI References")]
    public RawImage crosshair;
    public Image deathOverlay;
    public Text deathText;

    [Header("Cheats")]
    public bool enableChasers = true;
    public bool enableShooters = true;
    public bool invincibility = false;

    private void Start()
    {
        crosshair.enabled = true;
        deathOverlay.enabled = false;
        deathText.enabled = false;
    }

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

    public void TriggerDeath()
    {
        deathOverlay.enabled = true;
        deathText.enabled = true;
        crosshair.enabled = false;
    }
}
