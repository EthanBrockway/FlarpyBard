/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SFXLevel : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sliderValue;
    public void SetSfxLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("sfxvolume", sliderValue);
        Debug.Log(PlayerPrefs.GetFloat("sfxvolume", sliderValue));

        mixer.SetFloat("Musicvol", Mathf.Log10(sliderValue) * 20);
    }
}
*/