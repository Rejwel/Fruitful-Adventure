using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public static Inventory Instance { get; private set; }
   // pistol ammo, shotgun ammo, rifle ammo, minigun ammo
   public int[] bulletAmmount;
   private uint GrenadesAmmount = 0;
   private uint ShootingTurretAmmount = 0;
   private uint DetectingTurretAmmount = 0;
   public List<Gun> currentGuns = new List<Gun>();
   
   public Dictionary <string, uint> GameObjDictionary { get; set; }


   private bool doubleJump;
   private bool dash;
   private bool shield;

   private void Awake()
   {
      GameObjDictionary = new Dictionary<string, uint>();
      GameObjDictionary.Add("Turret", ShootingTurretAmmount);
      GameObjDictionary.Add("TurretDetecting", DetectingTurretAmmount);

      bulletAmmount = new int[] {999999, GunContainer.guns[1].GetMagazine(), GunContainer.guns[2].GetMagazine(), GunContainer.guns[3].GetMagazine()};
      currentGuns.Add(GunContainer.GetGun(0));
      doubleJump = false;
      dash = false;
      shield = false;
   }
   
   public uint LengthOfTurrets()
   {
      uint tempCount = 0;
      foreach (var key in GameObjDictionary)
      {
         print(key.Key + " " + key.Value);
         
         if (key.Value > 0) tempCount++;
      }

      return tempCount;
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
   public uint GetShootingTurret()
   {
      return ShootingTurretAmmount;
   }
   public void AddShootingTurret()
   {
      ShootingTurretAmmount++;
   }
   
   public void RemoveShootingTurret()
   {
      ShootingTurretAmmount--;
   }
   
   public uint GetDetectingTurret()
   {
      return DetectingTurretAmmount;
   }
   public void AddDetectingTurret()
   {
      DetectingTurretAmmount++;
   }
   
   public void RemoveDetectingTurret()
   {
      DetectingTurretAmmount--;
   }
   
   public uint GetGrenades()
   {
      return GrenadesAmmount;
   }
   public void AddGrenade()
   {
      GrenadesAmmount++;
   }
   
   public void RemoveGrenade()
   {
      GrenadesAmmount--;
   }
}
