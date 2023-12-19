using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSE : MonoBehaviour
{
    public AudioClip FirstSE;
    public AudioClip SecondSE;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayFirstSE();
    }

    void PlayFirstSE()
    {
        audioSource.clip = FirstSE;
        audioSource.Play();
    }

    System.Collections.IEnumerator WaitForFirstSE()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        PlaySecondSE();
    }

    void PlaySecondSE()
    {
        audioSource.clip = SecondSE;
        audioSource.Play();
    }
}
