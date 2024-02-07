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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void PlayPopSound()
    {
        if (popAudioSource == null && !TryGetComponent(out popAudioSource))
            return;
        popAudioSource.Stop();
        popAudioSource.clip = popSounds[UnityEngine.Random.Range(0, popSounds.Length)];
        popAudioSource.Play();
    }
}
