using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Handles the options and options menu for the game
//Makes use of PlayerPrefs to save options even when game is closed
public class OptionsMenuController : MonoBehaviour
{
    //We will leave one of these void
    //This lets us use the same script and prefab for both the options in the main menu and the game
    [Header("Controller References")]
    public GameController gameController;
    public MainMenuController mainMenuController;

    [Header("UI References")]
    public Button backButton;
    public Button resetButton;
    public TextMeshProUGUI masterVolumeText;
    public Slider masterVolumeSlider;
    public TextMeshProUGUI musicVolumeText;
    public Slider musicVolumeSlider;
    public TextMeshProUGUI sensitivityText;
    public Slider sensitivitySlider;

    void Start()
    {
        Hide();
        CheckPrefsExist();
    }

    //On the first time the game is loaded, player prefs will be empty
    //This will set them to default values if they are empty
    void CheckPrefsExist()
    {
        if (!PlayerPrefs.HasKey("sens")
            || !PlayerPrefs.HasKey("masterVolume")
            || !PlayerPrefs.HasKey("musicVolume")) SetDefaultPlayerPrefs();
    }
    
    public void SetDefaultPlayerPrefs()
    {
        //If one is empty, they will all be empty
        PlayerPrefs.SetFloat("sens", 1);
        PlayerPrefs.SetFloat("masterVolume", 1);
        PlayerPrefs.SetFloat("musicVolume", 1);
        SavePrefs();
        SetUIToPrefs();

        //Set volumes
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        gameController.setMusicVolume();
    }

    void SetUIToPrefs()
    {
        sensitivitySlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("sens"));
        masterVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume"));
        musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVolume"));
    }

    public void Back()
    {
        SavePrefs();
        Hide();
        if (mainMenuController == null) gameController.Pause();
        else mainMenuController.ShowMainMenu();
    }

    public void Hide()
    {
        disableButton(backButton);
        disableButton(resetButton);
        sensitivityText.enabled = false;
        sensitivitySlider.gameObject.SetActive(false);
        masterVolumeText.enabled = false;
        masterVolumeSlider.gameObject.SetActive(false);
        musicVolumeText.enabled = false;
        musicVolumeSlider.gameObject.SetActive(false);
    }

    public void Show()
    {
        enableButton(backButton);
        enableButton(resetButton);
        SetUIToPrefs();
        sensitivityText.enabled = true;
        sensitivitySlider.gameObject.SetActive(true);
        masterVolumeText.enabled = true;
        masterVolumeSlider.gameObject.SetActive(true);
        musicVolumeText.enabled = true;
        musicVolumeSlider.gameObject.SetActive(true);
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

    public void SetSens(float sens)
    {
        //Ignore the first time this is called (on start)
        //Hacky workaround, but I tried to do it by setting a bool, but couldn't get it to work properly
        //0.0101 is the starting value I set for the slider (but it will get changed by code and get set to the sens saved in prefs)
        if (sens == 0.0101f) return;
        PlayerPrefs.SetFloat("sens", sens);
    }

    public void SetMasterVolume(float volume)
    {
        if (volume == 0.0101f) return;
        PlayerPrefs.SetFloat("masterVolume", volume);
        AudioListener.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == 0.0101f) return;
        PlayerPrefs.SetFloat("musicVolume", volume);
        gameController.setMusicVolume();
    }

    public void SavePrefs() { PlayerPrefs.Save(); }
}