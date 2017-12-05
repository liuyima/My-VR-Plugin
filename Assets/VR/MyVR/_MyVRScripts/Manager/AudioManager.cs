using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public AudioSource audioS;
    public AudioClip buttonAudioC, selectAudioC, mainMenuAudioC;
    private void Start()
    {
    }
    public void PlayButtonAudio()
    {
        audioS.PlayOneShot(buttonAudioC);
    }
    public void PlaySelectAudio()
    {
        audioS.PlayOneShot(selectAudioC);
    }
    public void PlayMainMenuAudio()
    {
        audioS.PlayOneShot(mainMenuAudioC);
    }
    public void PlayAudio(AudioClip ac)
    {
        audioS.PlayOneShot(ac);
    }
}
