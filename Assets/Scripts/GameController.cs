using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Script for GameController, which handles UI and testing cheats, and will eventually handle settings
public class GameController : MonoBehaviour
{
    private bool paused;
    private bool gameOver;

    public string nextLevelName;
    public RawImage crosshair;

    [Header("Win/Lose UI References")]
    public Image winOverlay;
    public Image deathOverlay;
    public TextMeshProUGUI winLoseText;
    public Button mainMenuButton;
    public Button nextLevelButton;
    public Button retryButton;

    [Header("Pause UI References")]
    public Image pauseOverlay;
    public Button resumeButton;
    public Button optionsButton;

    [Header("Options References")]
    public OptionsMenuController optionsController;

    [Header("Cheats")]
    public bool enableChasers = true;
    public bool enableShooters = true;
    public bool invincibility = false;

    private void Start()
    {
        hideUI();
        lockCursor();
        crosshair.enabled = true;
        paused = false;
        gameOver = false;
    }

    void Update()
    {
        //Handle cheats
        if (Input.GetKeyDown(KeyCode.F)) enableChasers = !enableChasers;
        if (Input.GetKeyDown(KeyCode.G)) enableShooters = !enableShooters;
        if (Input.GetKeyDown(KeyCode.I)) invincibility = !invincibility;

        if (Input.GetKeyDown(KeyCode.R)) Retry();
        if (!gameOver && !paused && Input.GetKeyDown(KeyCode.Escape)) Pause();
        else if (!gameOver && !paused && Input.GetKeyDown(KeyCode.Escape)) UnPause();
    }

    public bool chasersEnabled() { return enableChasers; }

    public bool shootersEnabled() { return enableShooters; }

    public bool invincible() { return invincibility; }

    public void TriggerDeath()
    {
        crosshair.enabled = false;
        unlockCursor();

        //Move Main mainMenuButton to proper spot
        setButtonPosY(mainMenuButton, -150f);

        //Show UI
        deathOverlay.enabled = true;
        winLoseText.text = "YOU DIED";
        winLoseText.enabled = true;
        enableButton(mainMenuButton);
        enableButton(retryButton);

        gameOver = true;
    }

    public void TriggerWin()
    {
        crosshair.enabled = false;
        unlockCursor();

        //Move Main mainMenuButton to proper spot
        setButtonPosY(mainMenuButton, -150f);

        //Show UI
        winLoseText.enabled = true;
        enableButton(mainMenuButton);

        //Show next level button only if it is not the last level
        if(nextLevelName == "End")
        {
            winLoseText.text = "FINAL LEVEL\nCOMPLETE";
        }
        else
        {
            winLoseText.text = "LEVEL\nCOMPLETE";
            enableButton(nextLevelButton);
        }

        gameOver = true;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        crosshair.enabled = false;
        unlockCursor();

        //Move Main mainMenuButton to proper spot
        setButtonPosY(mainMenuButton, -75f);

        //Show UI
        pauseOverlay.enabled = true;
        enableButton(resumeButton);
        enableButton(optionsButton);
        enableButton(mainMenuButton);

        paused = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        crosshair.enabled = true;
        lockCursor();
        hideUI();
        optionsController.SavePrefs();
        optionsController.Hide();

        paused = false;
    }

    public void ShowOptions()
    {
        hideUI();
        pauseOverlay.enabled = true;
        optionsController.Show();
    }

    public void ToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void NextLevel() { SceneManager.LoadScene(nextLevelName); }

    public void Retry() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private void hideUI()
    {
        //Hide win/lose UI elements
        winOverlay.enabled = false;
        deathOverlay.enabled = false;
        winLoseText.enabled = false;
        disableButton(mainMenuButton);
        disableButton(nextLevelButton);
        disableButton(retryButton);

        //Hide pause UI elements
        pauseOverlay.enabled = false;
        disableButton(resumeButton);
        disableButton(optionsButton);
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

    void setButtonPosY(Button button, float posY)
    {
        RectTransform trans = button.GetComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, posY);
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

    public bool isPaused() { return paused; }
}