using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public static Inventory Instance { get; private set; }
   // pistol ammo, shotgun ammo, rifle ammo, minigun ammo
   public int[] bulletAmmount;
   private uint GrenadesAmmount = 0;
   private uint ShootingTurretAmmount = 4;
   private uint _slowingTurretAmmout = 0;
   private uint SlowTrapAmmount = 0;
   private uint DamageTrapAmmount = 0;
   private uint TrapFence = 0;
   
   public List<Gun> currentGuns = new List<Gun>();
   public List<GameObject> defendingStructures;
   
   public Dictionary <string, uint> GameObjDictionary { get; set; }


   private bool doubleJump;
   private bool dash;
   private bool shield;

   private void Awake()
   {
      defendingStructures = new List<GameObject>();
      GameObjDictionary = new Dictionary<string, uint>();
      GameObjDictionary.Add("Turret", ShootingTurretAmmount);
      GameObjDictionary.Add("TurretSlowing", _slowingTurretAmmout);
      GameObjDictionary.Add("SlowTrap", SlowTrapAmmount);
      GameObjDictionary.Add("DamageTrap", DamageTrapAmmount);
      GameObjDictionary.Add("TrapFence", TrapFence);

   }

   private void Start()
   {
      bulletAmmount = new int[] {999999, GunContainer.guns[1].GetMagazine(), GunContainer.guns[2].GetMagazine(), GunContainer.guns[3].GetMagazine(), GunContainer.guns[4].GetMagazine()};
      currentGuns.Add(GunContainer.GetGun(0));

      TrapFence = 2;
      SlowTrapAmmount = 2;
      _slowingTurretAmmout = 2;

      doubleJump = false;
      dash = false;
      shield = false;
   }

   public List<GameObject> getDefendingStructures()
   {
      return defendingStructures;
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
   
   public void AddSniper()
   {
      currentGuns.Add(GunContainer.GetGun(4));
   }
   public uint GetShootingTurret()
   {
      return ShootingTurretAmmount;
   }
   public void AddShootingTurret()
   {
      ShootingTurretAmmount++;
      GameObjDictionary["Turret"] = ShootingTurretAmmount;
   }
   
   public void RemoveShootingTurret()
   {
      if (ShootingTurretAmmount > 0)
      {
         ShootingTurretAmmount--;
         GameObjDictionary["Turret"] = ShootingTurretAmmount;
      }
   }
   
   public uint GetSlowingTurret()
   {
      return _slowingTurretAmmout;
   }
   public void AddSlowingTurret()
   {
      _slowingTurretAmmout++;
      GameObjDictionary["TurretSlowing"] = _slowingTurretAmmout;
   }
   
   public void RemoveSlowingTurret()
   {
      if (_slowingTurretAmmout > 0)
      {
         _slowingTurretAmmout--;
         GameObjDictionary["TurretSlowing"] = _slowingTurretAmmout;
      }
         
   }
   
    public uint GetSlowTrap()
    {
        return SlowTrapAmmount;
    }

    public void AddSlowTrap()
    {
        SlowTrapAmmount++;
        GameObjDictionary["SlowTrap"] = SlowTrapAmmount;
    }

    public void RemoveSlowTrap()
    {
        if (SlowTrapAmmount > 0)
        {
            SlowTrapAmmount--;
            GameObjDictionary["SlowTrap"] = SlowTrapAmmount;
        }
    }

    public uint GetDamageTrap()
    {
       return DamageTrapAmmount;
    }
    public void AddDamageTrap()
    {
       DamageTrapAmmount++;
       GameObjDictionary["DamageTrap"] = DamageTrapAmmount;
    }

    public void RemoveDamageTrap()
    {
       if (DamageTrapAmmount > 0)
       {
          DamageTrapAmmount--;
          GameObjDictionary["DamageTrap"] = DamageTrapAmmount;
       }
    }

    public uint GetTrapFence()
    {
       return TrapFence;
    }

    public void AddTrapFence()
    {
       TrapFence++;
       GameObjDictionary["TrapFence"] = TrapFence;
    }

    public void RemoveTrapFence()
    {
       if (TrapFence > 0)
       {
          TrapFence--;
          GameObjDictionary["TrapFence"] = TrapFence;
       }
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
