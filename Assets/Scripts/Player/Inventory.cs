using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   // pistol ammo, shotgun ammo, rifle ammo, minigun ammo
   public int[] bulletAmmount;
   public List<Gun> currentGuns = new List<Gun>();


   private bool doubleJump;
   private bool dash;
   private bool shield;

   private void Awake()
   {
      bulletAmmount = new int[] {999999, GunContainer.guns[1].GetMagazine(), GunContainer.guns[2].GetMagazine(), GunContainer.guns[3].GetMagazine()};
      currentGuns.Add(GunContainer.GetGun(0));
      doubleJump = false;
      dash = false;
      shield = false;
   }

   public bool CanDoubleJump()
   {
      return doubleJump;
   }
   public bool CanDash()
   {
      return dash;
   }

   public bool isShielded()
   {
      return shield;
   }

   public void activeDash()
   {
      dash = true;
   }

   public void activeDoubleJump()
   {
      doubleJump = true;
   }

   public void activeShield()
   {
      shield = true;
   }

   public void removeShield()
   {
      shield = false;
   }

   public void AddShotgun()
   {
      currentGuns.Add(GunContainer.GetGun(1));
   }
   
   public void AddRifle()
   {
      currentGuns.Add(GunContainer.GetGun(2));
   }
   
   public void AddMinigun()
   {
      currentGuns.Add(GunContainer.GetGun(3));
   }
   
}
