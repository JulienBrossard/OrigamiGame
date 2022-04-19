using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    #region Declarations
    
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource[] audioSources;

    public static AudioManager instance;

    #endregion
    
    private void Awake()
    {
        instance = this;
    }
    
    public void PlaySound(AudioClip sound, float volume, float speed, float time)
    {

        #region Audiosource free
        
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null || !audioSources[i].isPlaying)
            {
                audioSources[i].clip = sound;
                audioSources[i].volume = volume;
                audioSources[i].time = time;
                audioSources[i].pitch = speed;
                audioSources[i].Play();
                return;
            }
        }
        
        #endregion

        #region Audiosource not free

        audioSources[0].clip = sound;
        audioSources[0].volume = volume;
        audioSources[0].time = time;
        audioSources[0].pitch = speed;
        
        #endregion
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
