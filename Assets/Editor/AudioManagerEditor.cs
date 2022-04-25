using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AudioManager script = (AudioManager) target;
        AudioSource currentAudioSource;

        if (script.audioSourceLenght != script.audioSources.Length)
        {
            if (script.audioSourceLenght < script.audioSources.Length)
            {
                for (int i = script.audioSourceLenght; i <  script.audioSources.Length ; i++)
                {
                    currentAudioSource = script.gameObject.AddComponent<AudioSource>();
                    script.audioSources[i] = currentAudioSource;
                }
            }
            else
            {
                for (int k = 0; k < script.audioSourcesEditor.Length- script.audioSources.Length ; k++)
                {
                    Debug.Log("ok");
                    for (int i = 0; i < script.audioSourcesEditor.Length; i++)
                    {
                        for (int j = 0; j < script.audioSources.Length; j++)
                        {
                            if (script.audioSourcesEditor[i].GetInstanceID() == script.audioSources[j].GetInstanceID())
                            {
                                goto CONTINUE;
                            }
                        }
                        DestroyImmediate(script.audioSourcesEditor[i]);
                        script.audioSourcesEditor.ToList().Remove(script.audioSourcesEditor[i]);
                        script.audioSourcesEditor.ToArray();
                        CONTINUE : continue;
                    }
                }
            }
            
            script.audioSourcesEditor = script.audioSources;
            script.audioSourceLenght = script.audioSources.Length;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
