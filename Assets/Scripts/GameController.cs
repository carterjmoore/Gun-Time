using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script for GameController, which handles UI and testing cheats, and will eventually handle settings
public class GameController : MonoBehaviour
{
    bool dead;

    [Header("UI References")]
    public RawImage crosshair;
    public Image deathOverlay;
    public Text deathText;

    [Header("Cheats")]
    public bool enableChasers = true;
    public bool enableShooters = true;
    public bool invincibility = false;

    public AudioSource deathSound;
    public AudioSource BGM1;

    private void Start()
    {
        crosshair.enabled = true;
        deathOverlay.enabled = false;
        deathText.enabled = false;
        dead = false;
        BGM1.loop = true;
        BGM1.Play();
    }

    void Update()
    {
        if (dead && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        deathSound.Play();
        deathOverlay.enabled = true;
        deathText.enabled = true;
        crosshair.enabled = false;
        dead = true;
    }
}
