using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCollection : MonoBehaviour
{
    [Header("========== Output ==========")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [Header("========== Background Music ==========")]
    public AudioClip mainMenu;
    public AudioClip pregame;
    public AudioClip office;
    public AudioClip gameOver;
    
    [Header("========== SFX ==========")]
    public AudioClip UIButtonClick;
    public AudioClip typing;
    public AudioClip walking;
    public AudioClip statsChange;

    private static AudioCollection instance;

    private void Awake()
    {
        // Ensure there's only one instance of AudioCollection
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }

    public void StopPlayBGM()
    {
        BGM.Stop();
    }

    public void PauseBGM()
    {
        if (BGM.isPlaying)
        {
            BGM.Pause();
        }
    }

    public void ResumeBGM()
    {
        if (!BGM.isPlaying)
        {
            BGM.UnPause();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        if (SFX.isPlaying)
        {
            SFX.Stop();
        }
    }
}
