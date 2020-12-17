using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   // pistol ammo, shotgun ammo, rifle ammo, minigun ammo
   public int[] bulletAmmount;



   private void Awake()
   {
      bulletAmmount = new int[] {60, 5, 5, 5};
   }
   
   
   
   
}
