using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour
{
    public static readonly Gun pistol = new Gun(0.35f, 1, 0.15f, 9, "Pisol", 20);
    public static readonly Gun shotgun = new Gun(0.35f, 1, 0.15f, 9, "Shotgun", 20);
    public static readonly Gun rifle = new Gun(0.35f, 1, 0.15f, 9, "Rifle", 20);
    public static readonly Gun minigun = new Gun(0.35f, 1, 0.15f, 9, "Minigun", 20);

    public static List<Gun> guns;

    private void Start()
    {
        guns = new List<Gun>();
        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(rifle);
        guns.Add(minigun);
    }

    public static Gun GetGun(int n)
    {
        return guns[n];
    }
}
