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
    public AudioClip office;
    public AudioClip clientMeeting;
    public AudioClip gameOver;
    
    [Header("========== SFX ==========")]
    public AudioClip UIButtonClick;
    public AudioClip UIBackButtonClick;
    public AudioClip wokerWorking;
    public AudioClip peopleSpeaking;
    public AudioClip dillema;
    public AudioClip statsUp;
    public AudioClip statsDown;

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
