using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    //Main menu stuff
    [Header("Main Menu References")]
    public TextMeshProUGUI titleText;
    public Button levelsButton;
    public Button optionsButton;
    public Button quitButton;

    [Header("Level References")]
    //Level select stuff
    public Button introductionButton;
    public Button chasmButton;
    public Button gauntletButton;
    public Button hallwayButton;
    public Button lavaButton;
    public Button ambushButton;
    public Button towerButton;
    public Button backButton;

    //Options menu
    [Header("Options")]
    public OptionsMenuController optionsController;

    //Audio
    [Header("Audio")]
    public AudioSource BGM;
    float musicVolumeDefault;

    void Start()
    {
        ShowMainMenu();

        //Get and set audio volume values
        musicVolumeDefault = BGM.volume;
        setMusicVolume();
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume", 1f);

        //Play background music on loop
        BGM.loop = true;
        BGM.Play();
    }

    public void ShowMainMenu()
    {
        //Show menu stuff
        titleText.enabled = true;
        enableButton(levelsButton);
        enableButton(optionsButton);
        enableButton(quitButton);

        //Hide level select stuff
        disableButton(introductionButton);
        disableButton(chasmButton);
        disableButton(gauntletButton);
        disableButton(hallwayButton);
        disableButton(lavaButton);
        disableButton(ambushButton);
        disableButton(towerButton);
        disableButton(backButton);

        //Hide options
        optionsController.Hide();
    }

    public void ShowLevelSelect()
    {
        //Hide menu stuff
        titleText.enabled = false;
        disableButton(levelsButton);
        disableButton(optionsButton);
        disableButton(quitButton);

        //Show level select stuff
        enableButton(introductionButton);
        enableButton(chasmButton);
        enableButton(gauntletButton);
        enableButton(hallwayButton);
        enableButton(lavaButton);
        enableButton(ambushButton);
        enableButton(towerButton);
        enableButton(backButton);
    }

    public void ShowOptions()
    {
        titleText.enabled = false;
        disableButton(levelsButton);
        disableButton(optionsButton);
        disableButton(quitButton);

        optionsController.Show();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadIntroduction()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void LoadChasm()
    {
        SceneManager.LoadScene("Chasm");
    }

    public void LoadGauntlet()
    {
        SceneManager.LoadScene("Gauntlet");
    }

    public void LoadHallway()
    {
        SceneManager.LoadScene("HallwayOfDeath");
    }

    public void LoadLava()
    {
        SceneManager.LoadScene("FloorIsLava");
    }

    public void LoadAmbush()
    {
        SceneManager.LoadScene("Ambush");
    }

    public void LoadTower()
    {
        SceneManager.LoadScene("Tower");
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

    //Set music volume according to player prefs
    public void setMusicVolume() { BGM.volume = musicVolumeDefault * PlayerPrefs.GetFloat("musicVolume", 1f); }
}
