using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip pistol, rifle, shotgun, minigun, pistolEmpty, gunEmpty;
    private static AudioSource audioSrc;

    private void Start()
    {
        pistol = Resources.Load<AudioClip>("Pistol");
        pistolEmpty = Resources.Load<AudioClip>("Pistol_empty");
        gunEmpty = Resources.Load<AudioClip>("Gun_empty");
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
            case "Pistol_empty":
                audioSrc.PlayOneShot(pistolEmpty);
                break;
            case "Gun_empty":
                audioSrc.PlayOneShot(gunEmpty);
                break;
        }
    }
}
