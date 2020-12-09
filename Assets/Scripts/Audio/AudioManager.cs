using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip pistol, rifle, shotgun, minigun;
    private static AudioSource audioSrc;

    private void Start()
    {
        pistol = Resources.Load<AudioClip>("Pistol");
        shotgun = Resources.Load<AudioClip>("Pistol");
        rifle = Resources.Load<AudioClip>("Rifle");
        minigun = Resources.Load<AudioClip>("Minigun");
        

        audioSrc = GetComponent<AudioSource>();
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "Pistol":
                audioSrc.PlayOneShot(pistol);
                break;
            case "Shotgun":
                audioSrc.PlayOneShot(shotgun);
                break;
            case "Rifle":
                audioSrc.PlayOneShot(rifle);
                break;
            case "Minigun":
                audioSrc.PlayOneShot(minigun);
                break;
        }
    }
}
