using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    #region Declarations

    [Tooltip("L'audio mixer du jeu, à récupérer dans le dossier Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private Slider sliderSensibility;
    [SerializeField] private Slider sliderMainVolume;
    private float mainVolumeValue;
    private bool result;

    #endregion

    private void Start()
    {
        sliderSensibility.value = (PlayerPrefs.GetInt ("Sensibility", 45)-20)/50f;
        result = audioMixer.GetFloat("MainVolume", out mainVolumeValue);
        sliderMainVolume.value = Mathf.Pow(10, mainVolumeValue / 20);
    }

    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Clamp(volume,0.01f,1f)) * 20);
    }
    
    public void EffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(Mathf.Clamp(volume,0.01f,1f)) * 20);
    }
    
    public void Music(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(volume,0.01f,1f)) * 20);
    }

    public void Sensibility()
    {
        PlayerPrefs.SetInt("Sensibility",(int) (20+sliderSensibility.value*50));
        PlayerPrefs.Save();
    }
}
