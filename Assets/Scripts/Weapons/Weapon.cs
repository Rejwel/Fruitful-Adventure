using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Gun gun;
    public int gunNumber;

    private void Start()
    {
        gun = GunContainer.GetGun(gunNumber);
    }

    public Gun GetGun()
    {
        return gun;
    }
    
}
