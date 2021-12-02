using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip pistol, rifle, shotgun, minigun, pistolEmpty, gunEmpty, gunReload, enemyMelee, enemyRanged, enemyMage;
    private static AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pistol = Resources.Load<AudioClip>("Pistol");
        gunReload = Resources.Load <AudioClip>("Gun_reload");
        pistolEmpty = Resources.Load<AudioClip>("Pistol_empty");
        gunEmpty = Resources.Load<AudioClip>("Gun_empty");
        shotgun = Resources.Load<AudioClip>("Pistol");
        rifle = Resources.Load<AudioClip>("Rifle");
        minigun = Resources.Load<AudioClip>("Minigun");
        enemyMelee = Resources.Load<AudioClip>("enemyMelee");
        enemyRanged = Resources.Load<AudioClip>("enemyRanged");
        enemyMage = Resources.Load<AudioClip>("enemyMage");
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
            case "Sniper":
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
            case "Gun_reload":
                audioSrc.PlayOneShot(gunReload);
                break;
            case "enemyMelee":
                audioSrc.PlayOneShot(enemyMelee);
                break;
            case "enemyRanged":
                audioSrc.PlayOneShot(enemyRanged);
                break;
            case "enemyMage":
                audioSrc.PlayOneShot(enemyMage);
                break;
        }
    }
    public static void stopSound()
    {
        audioSrc.Stop();
    }
}

