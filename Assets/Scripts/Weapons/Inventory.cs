using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Gun gun;
    
    public Gun GetFirstGun()
    {
        return GunContainer.GetGun(0);
    }
}
