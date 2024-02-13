using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
   
    public AudioMixer mixer;
    public void SetVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("Musicvol", Mathf.Log10(sliderValue) * 20);
    }


}
