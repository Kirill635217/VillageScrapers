using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] popSounds;
    [SerializeField] private AudioSource popAudioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayPopSound()
    {
        popAudioSource.Stop();
        popAudioSource.clip = popSounds[UnityEngine.Random.Range(0, popSounds.Length)];
        popAudioSource.Play();
    }
}
