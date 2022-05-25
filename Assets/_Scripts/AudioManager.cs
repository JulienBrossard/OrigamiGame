using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    #region Declarations
    
    [Header("Wind Sounds")] [SerializeField]
    private AudioClip windPlane;
    [SerializeField] private AudioClip windCloud;
    [SerializeField] private AudioSource windAudioSource;
    
    [Header("Sounds Effects")]
    [Tooltip("La liste des AudioSources qui vont jouer les effets sonores du jeu, à récupérer directement sur l'objet actuel")]
    [SerializeField] public AudioSource[] audioSources;

    [Header("UI Sounds")] 
    [SerializeField] private AudioClip[] UISounds;

    public static AudioManager instance;
    [HideInInspector] public int audioSourceLenght = 0;
    [HideInInspector] public AudioSource[] audioSourcesEditor; 

    #endregion
    
    private void Awake()
    {
        instance = this;
    }


    private void OnMouseUpAsButton()
    {
        PlaySound(UISounds[Random.Range(0, UISounds.Length)],1,1,0);
    }

    public void ChangeWindSound()
    {
        if (windAudioSource.clip == windPlane)
        {
            windAudioSource.Stop();
            windAudioSource.clip = windCloud;
            windAudioSource.Play();
            return;
        }
        windAudioSource.Stop();
        windAudioSource.clip = windPlane;
        windAudioSource.Play();
    }

    // Joue le son
    public void PlaySound(AudioClip sound, float volume, float speed, float time)
    {

        #region Audiosource free
        
        // Cherche une place de libre dans l'array
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
        
        //Si pas de place remplace le son le plus ancien
        
        Tools.instance.Sort(audioSources);
        audioSources[audioSources.Length-1].clip = sound;
        audioSources[audioSources.Length-1].volume = volume;
        audioSources[audioSources.Length-1].time = time;
        audioSources[audioSources.Length-1].pitch = speed;
        
        #endregion
    }
    public void PlayUISound(AudioClip sound)
    {

        #region Audiosource free
        
        // Cherche une place de libre dans l'array
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null || !audioSources[i].isPlaying)
            {
                audioSources[i].clip = sound;
                audioSources[i].Play();
                return;
            }
        }
        
        #endregion

        #region Audiosource not free
        
        //Si pas de place remplace le son le plus ancien
        
        Tools.instance.Sort(audioSources);
        audioSources[audioSources.Length-1].clip = sound;

        #endregion
    }
    

    
    // Arrête tous les sons
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
