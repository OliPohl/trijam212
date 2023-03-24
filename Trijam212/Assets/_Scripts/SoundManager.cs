using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip errorHard;
    [SerializeField] private AudioClip errorMiddle;
    [SerializeField] private AudioClip errorSoft;
    [SerializeField] private AudioClip blocked;
    [SerializeField] private AudioClip click;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        sfxAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        // sfxAudioSource.volume = 0.5f;
    }


    public void PlayClickSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(click);
    }

    public void PlayBlockedSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(blocked);
    }

    public void PlayErrorSoftSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(errorSoft);
    }

    public void PlayErrorMiddleSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(errorMiddle);
    }

    public void PlayErrorHardSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(errorHard);
    }



    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
