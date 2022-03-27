using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

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
}
