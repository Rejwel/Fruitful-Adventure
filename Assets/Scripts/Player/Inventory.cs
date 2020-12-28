using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   // pistol ammo, shotgun ammo, rifle ammo, minigun ammo
   public int[] bulletAmmount;


   private bool doubleJump;
   private bool dash;
   private bool shield;

   private void Awake()
   {
      bulletAmmount = new int[] {999, 999, 999, 999};
      doubleJump = true;
      dash = true;
   }

   public bool CanDoubleJump()
   {
      return doubleJump;
   }
   public bool CanDash()
   {
      return dash;
   }
   
}
