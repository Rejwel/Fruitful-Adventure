using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private static AudioSource audioSrc;
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.loop = true;
        audioSrc.clip = Resources.Load<AudioClip>("Background");
        audioSrc.volume = PlayerPrefs.GetFloat("music_slider_value");
        audioSrc.Play();
    }
}
