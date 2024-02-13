using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsScreen;
    public GameObject mainMenuScreen;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider voiceSlider;
    public void SaveSettings()
    {

        PlayerPrefs.SetFloat("musicvolume", musicSlider.value);
        PlayerPrefs.SetFloat("sfxvolume", sfxSlider.value);
        PlayerPrefs.SetFloat("voicevolume", voiceSlider.value);
        PlayerPrefs.Save();
    }
    public void closeOptions()
    {
        SaveSettings();
        optionsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
 
}
