using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunModels;
    
    public static List<Gun> guns;

    public static readonly Gun pistol = new Gun(0,0.5f, 1, 0f, 500, "Pistol", 20, 10, null);
    public static readonly Gun shotgun = new Gun(1,1f, 5, 3.5f, 500, "Shotgun", 20, 15, null);
    public static readonly Gun rifle = new Gun(2,0.1f, 1, 1f, 500, "Rifle", 30, 60, null);
    public static readonly Gun minigun = new Gun(3,0.05f, 1, 2.5f, 500, "Minigun", 20, 120, null);
    public static readonly Gun sniper = new Gun(4,1f, 1, 0f, 800, "Sniper", 100, 10, null);

    private void Awake()
    {
        pistol.setGunModel(gunModels[0]);
        shotgun.setGunModel(gunModels[0]);
        rifle.setGunModel(gunModels[0]);
        minigun.setGunModel(gunModels[0]);
        sniper.setGunModel(gunModels[1]);

        guns = new List<Gun>();
        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(rifle);
        guns.Add(minigun);
        guns.Add(sniper);
    }

    public static Gun GetGun(int n)
    {
        return guns[n];
    }
}
