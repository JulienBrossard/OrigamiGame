using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_LocalAudioManager : MonoBehaviour
{
    AudioSource localSource;
    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        localSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int audioClipNum)
    {
        localSource.clip = audioClips[audioClipNum];
        localSource.Play();
    }
}
