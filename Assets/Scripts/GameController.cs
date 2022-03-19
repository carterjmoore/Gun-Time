using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Script for GameController, which handles UI and testing cheats, and will eventually handle settings
public class GameController : MonoBehaviour
{
    public string nextLevelName;
    public RawImage crosshair;

    [Header("Win/Lose UI References")]
    public Image winOverlay;
    public Image deathOverlay;
    public TextMeshProUGUI winLoseText;
    public Button mainMenuButton;
    public Button nextLevelButton;
    public Button retryButton;

    [Header("Cheats")]
    public bool enableChasers = true;
    public bool enableShooters = true;
    public bool invincibility = false;

    private void Start()
    {
        hideUI();
        lockCursor();
        crosshair.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) enableChasers = !enableChasers;
        if (Input.GetKeyDown(KeyCode.G)) enableShooters = !enableShooters;
        if (Input.GetKeyDown(KeyCode.I)) invincibility = !invincibility;
    }

    public bool chasersEnabled() { return enableChasers; }

    public bool shootersEnabled() { return enableShooters; }

    public bool invincible() { return invincibility; }

    public void TriggerDeath()
    {
        crosshair.enabled = false;
        unlockCursor();

        //Show UI
        deathOverlay.enabled = true;
        winLoseText.text = "YOU DIED";
        winLoseText.enabled = true;
        enableButton(mainMenuButton);
        enableButton(retryButton);
    }

    public void TriggerWin()
    {
        crosshair.enabled = false;
        unlockCursor();

        //Show UI
        winOverlay.enabled = true;
        winLoseText.text = "LEVEL\nCOMPLETE";
        winLoseText.enabled = true;
        enableButton(mainMenuButton);
        enableButton(nextLevelButton);
    }

    public void ToMainMenu() { SceneManager.LoadScene("MainMenu"); }

    public void NextLevel() { SceneManager.LoadScene(nextLevelName); }

    public void Retry() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    private void hideUI()
    {
        winOverlay.enabled = false;
        deathOverlay.enabled = false;
        winLoseText.enabled = false;
        disableButton(mainMenuButton);
        disableButton(nextLevelButton);
        disableButton(retryButton);
    }

    void disableButton(Button button)
    {
        button.image.enabled = false;
        button.enabled = false;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    void enableButton(Button button)
    {
        button.image.enabled = true;
        button.enabled = true;
        button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
