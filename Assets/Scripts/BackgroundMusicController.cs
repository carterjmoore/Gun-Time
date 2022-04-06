using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource BGM1;
    float musicVolumeDefault;
    // Start is called before the first frame update
    void Start()
    {
        musicVolumeDefault = BGM1.volume;
        setMusicVolume();
        BGM1.loop = true;
        BGM1.Play();
    }

    public void setMusicVolume() { BGM1.volume = musicVolumeDefault * PlayerPrefs.GetFloat("musicVolume", 1f); }
}