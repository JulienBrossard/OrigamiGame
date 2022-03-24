using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource[] audioSources;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip sound, float volume, float speed, float time)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null || !audioSources[i].isPlaying)
            {
                audioSources[i].clip = sound;
                audioSources[i].volume = volume;
                audioSources[i].time = time;
                audioSources[i].pitch = speed;
                return;
            }
        }
        audioSources[0].clip = sound;
        audioSources[0].volume = volume;
        audioSources[0].time = time;
        audioSources[0].pitch = speed;
    }

    public void StopSounds()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null)
            {
                audioSources[i].Stop();
            }
        }
    }
    
}
