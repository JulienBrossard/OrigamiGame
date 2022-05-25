using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_Music : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] private AudioClip[] allClips, clipsOrder;
    [SerializeField] float secondsToNewClip = 2f;
    int currentlipPlaying = 0;

    void Start()
    {
        RandomizeClipsOrder();
        StartCoroutine(PlayDaMusique());
    }

    public void RandomizeClipsOrder()
    {
        for (int i = 0; i < allClips.Length; i++)
        {
            int randomVal;
            randomVal = Random.Range(0, allClips.Length);
            clipsOrder[i] = allClips[randomVal];
        }
    }

    IEnumerator PlayDaMusique()
    {
        if(currentlipPlaying < clipsOrder.Length)
        {
            currentlipPlaying++;
        }
        else
        {
            currentlipPlaying = 0;
            RandomizeClipsOrder();
        }
        musicSource.clip = clipsOrder[currentlipPlaying];
        musicSource.Play();
        yield return new WaitForSeconds(secondsToNewClip);
        StartCoroutine(PlayDaMusique());
    }

    public void StopDaMusic()
    {
        musicSource.enabled = false;
        StopCoroutine(PlayDaMusique());
    }
}
