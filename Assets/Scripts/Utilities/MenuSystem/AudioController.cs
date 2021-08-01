using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioController Instance { get { return Instance; } }
    AudioController instance;

    public AudioMixer audioMixer;

    void Start()
    {
        instance = this;

        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        audioMixer?.SetFloat("MasterVolume", masterVolume);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
        audioMixer?.SetFloat("MusicVolume", musicVolume);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
        audioMixer?.SetFloat("SFXVolume", sfxVolume);
    }

    public void SetMasterVolume(float level)
    {
        //Debug.Log("Master volume level: " + level);
        audioMixer?.SetFloat("MasterVolume", Mathf.Log(level) * 20);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void SetMusicVolume(float level)
    {

        //Debug.Log("Music volume level: " + level);
        audioMixer?.SetFloat("MusicVolume", Mathf.Log(level) * 20);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    public void SetSFXVolume(float level)
    {

        //Debug.Log("SFX volume level: " + level);
        audioMixer?.SetFloat("SFXVolume", Mathf.Log(level) * 20);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }
}
