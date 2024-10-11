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
    public AudioClip dungeon;
    public AudioClip dungeon2;
    public AudioClip guild;
    public AudioClip shop;
    public AudioClip battle;
    
    [Header("========== SFX ==========")]
    public AudioClip UIButtonClick;
    public AudioClip UIBackButtonClick;
    public AudioClip slash;
    public AudioClip buy;
    public AudioClip hurt;

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

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

}
