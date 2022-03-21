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
    public TextMeshProUGUI sensitivityText;
    public Slider sensitivitySlider;

    void Start()
    {
        Hide();
        CheckPrefsExist();
    }

    //On the first time the game is loaded, player prefs will be empty
    //This well set them to default values if they are empty
    void CheckPrefsExist()
    {
        if (!PlayerPrefs.HasKey("sens")) SetDefaultPlayerPrefs();
    }
    
    public void SetDefaultPlayerPrefs()
    {
        //If one is empty, they will all be empty
        PlayerPrefs.SetFloat("sens", 1);
        SavePrefs();
        SetUIToPrefs();
    }

    void SetUIToPrefs()
    {
        sensitivitySlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("sens"));
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
    }

    public void Show()
    {
        enableButton(backButton);
        enableButton(resetButton);
        SetUIToPrefs();
        sensitivityText.enabled = true;
        sensitivitySlider.gameObject.SetActive(true);
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

    public void SavePrefs() { PlayerPrefs.Save(); }
}