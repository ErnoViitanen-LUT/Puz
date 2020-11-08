using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider effectsVolume;

    public void Start(){
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        masterVolume.value = volume;
        audioMixer.GetFloat("MusicVolume", out volume);
        musicVolume.value = volume;
        audioMixer.GetFloat("EffectsVolume", out volume);
        effectsVolume.value = volume;
    }
    public void SetMasterVolume(float volume){
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume){
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetEffectsVolume(float volume){
        audioMixer.SetFloat("EffectsVolume", volume);
    }

}
