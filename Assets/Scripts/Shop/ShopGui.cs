using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class ShopGui : MonoBehaviour
{
    // ammo * 10 price
    private int shotgunPrice = 350;
    private int riflePrice = 400;
    private int minigunPrice = 500;
    
    private int shotgunAmmoPrice = 5;
    private int rifleAmmoPrice = 1;
    private int minigunAmmoPrice = 2;
    
    private int dashPrice = 200;
    private int dJumpPrice = 200;
    private int ShieldPrice = 50;
    
    private int hp25Price = 50;
    private int hp50Price = 90;
    private int hp75Price = 130;
    private int hp100Price = 160;

    private int GrenadePrice = 75;
    private int ShootingTurretPrice = 75;
    private int DetectingTurretPrice = 50;
    
    private TextMeshProUGUI currMoney;
    private TextMeshProUGUI currPistolAmmo;
    private TextMeshProUGUI currShotgunAmmo;
    private TextMeshProUGUI currRifleAmmo;
    private TextMeshProUGUI currMinigunAmmo;
    private TextMeshProUGUI currGrenades;
    private TextMeshProUGUI currShootingTurrets;
    private TextMeshProUGUI currDetectingTurrets;
    private ColorBlock GreenColorDisabled;

    private GameObject dash;
    private GameObject dJump;
    private GameObject shield;

    private GameObject shotgun;
    private GameObject rifle;
    private GameObject minigun;

    public HealthBarScript healthBarPlayer;
    public HealthBarScript healthBarMenu;
    private HealthPlayer healthPlayer;

    private Inventory inv;
    private Money money;
    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        healthPlayer = FindObjectOfType<HealthPlayer>();
        currMoney = GameObject.Find("CurrMoney").GetComponent<TextMeshProUGUI>();
        currShotgunAmmo = GameObject.Find("CurrShotgunAmmo").GetComponent<TextMeshProUGUI>();
        currRifleAmmo = GameObject.Find("CurrRifleAmmo").GetComponent<TextMeshProUGUI>();
        currMinigunAmmo = GameObject.Find("CurrMinigunAmmo").GetComponent<TextMeshProUGUI>();
        
        currGrenades = GameObject.Find("CurrGrenades").GetComponent<TextMeshProUGUI>();
        currShootingTurrets = GameObject.Find("CurrShootingTurrets").GetComponent<TextMeshProUGUI>();
        currDetectingTurrets = GameObject.Find("CurrDetectingTurrets").GetComponent<TextMeshProUGUI>();

        dash = GameObject.Find("BuyDash");
        dJump = GameObject.Find("BuyDoubleJump");
        shield = GameObject.Find("BuyShield");

        shotgun = GameObject.Find("BuyShotgun");
        rifle = GameObject.Find("BuyRifle");
        minigun = GameObject.Find("BuyMinigun");
        
        money = FindObjectOfType<Money>();
        money.CurrentMoney += 140;
    }
    
    void Update()
    {
        // for shield
        if (inv.isShielded())
        {
            shield.GetComponent<Button>().interactable = false;
            shield.GetComponentsInChildren<Image>()[0].color = Color.green;
        }
        else
        {
            shield.GetComponent<Button>().interactable = true;
            shield.GetComponentsInChildren<Image>()[0].color = Color.white;
        }
        
        // for dash
        if (inv.CanDash())
        {
            dash.GetComponent<Button>().interactable = false;
            dash.GetComponentsInChildren<Image>()[0].color = Color.green;
        }

        // for dJump
        if(inv.CanDoubleJump())
        {
            dJump.GetComponent<Button>().interactable = false;
            dJump.GetComponentsInChildren<Image>()[0].color = Color.green;
        }
        
        // for weaponShotgun
        if(inv.currentGuns.Exists(x => x.GetId() == 1))
        {
            shotgun.GetComponent<Button>().interactable = false;
        }
        
        // for weaponRifle
        if(inv.currentGuns.Exists(x => x.GetId() == 2))
        {
            rifle.GetComponent<Button>().interactable = false;
        }
        // for weaponMinigun
        if(inv.currentGuns.Exists(x => x.GetId() == 3))
        {
            minigun.GetComponent<Button>().interactable = false;
        }

        // TEXT
        currMoney.text = $"= {money.CurrentMoney}";
        currGrenades.text = $"(75) Grenade {inv.GetGrenades()}";
        currShootingTurrets.text = $"(75) Turret {inv.GetShootingTurret()}";
        currDetectingTurrets.text = $"(75) Detecting Turret {inv.GetDetectingTurret()}";
        

        if(inv.bulletAmmount[1] > 10000)
            currShotgunAmmo.text = "(50) Current : ∞";
        else
            currShotgunAmmo.text = $"(50) Current : {inv.bulletAmmount[1]}";
        
        if(inv.bulletAmmount[2] > 10000)
            currRifleAmmo.text = "(10) Current : ∞";
        else
            currRifleAmmo.text = $"(10) Current : {inv.bulletAmmount[2]}";
        
        if(inv.bulletAmmount[3] > 10000)
            currMinigunAmmo.text = "(20) Current : ∞";
        else
            currMinigunAmmo.text = $"(20) Current : {inv.bulletAmmount[3]}";

    }

    public void BuyShotgun()
    {
        if (money.CurrentMoney >= shotgunPrice)
        {
            money.CurrentMoney -= shotgunPrice;
            inv.AddShotgun();
        }
    }
    
    public void BuyRifle()
    {
        if (money.CurrentMoney >= riflePrice)
        {
            money.CurrentMoney -= riflePrice;
            inv.AddRifle();
        }
    }
    
    public void BuyMinigun()
    {
        if (money.CurrentMoney >= minigunPrice)
        {
            money.CurrentMoney -= minigunPrice;
            inv.AddMinigun();
        }
    }


    public void BuyDoubleJump()
    {
        if (money.CurrentMoney >= dJumpPrice)
        {
            money.CurrentMoney -= dJumpPrice;
            inv.activeDoubleJump();
        }
    }
    
    public void BuyDash()
    {
        if (money.CurrentMoney >= dashPrice)
        {
            money.CurrentMoney -= dashPrice;
            inv.activeDash();
        }
    }
    
    public void BuyShiled()
    {
        if (money.CurrentMoney >= ShieldPrice)
        {
            money.CurrentMoney -= ShieldPrice;
            inv.activeShield();
        }
    }
    
    public void BuyGrenade()
    {
        if (money.CurrentMoney >= GrenadePrice)
        {
            money.CurrentMoney -= GrenadePrice;
            inv.AddGrenade();
        }
    }
    
    public void BuyShootingTurret()
    {
        if (money.CurrentMoney >= ShootingTurretPrice)
        {
            money.CurrentMoney -= ShootingTurretPrice;
            inv.AddShootingTurret();
            inv.GameObjDictionary.Remove("Turret");
            inv.GameObjDictionary.Add("Turret", inv.GetShootingTurret());
        }
    }
    
    public void BuyDetectingTurret()
    {
        if (money.CurrentMoney >= DetectingTurretPrice)
        {
            money.CurrentMoney -= DetectingTurretPrice;
            inv.AddDetectingTurret();
            inv.GameObjDictionary.Remove("TurretDetecting");
            inv.GameObjDictionary.Add("TurretDetecting", inv.GetDetectingTurret());
        }
    }
    
    public void BuyShotgunAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * shotgunAmmoPrice)
        {
            money.CurrentMoney -= ammo * shotgunAmmoPrice;
            inv.bulletAmmount[1] += ammo;
        }
    }
    
    public void BuyRifleAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * rifleAmmoPrice)
        {
            money.CurrentMoney -= ammo * rifleAmmoPrice;
            inv.bulletAmmount[2] += ammo;
        }
    }
    
    public void BuyMinigunAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * minigunAmmoPrice)
        {
            money.CurrentMoney -= ammo * minigunAmmoPrice;
            inv.bulletAmmount[3] += ammo;
        }
    }

    public void Buy25Hp()
    {
        if (money.CurrentMoney >= hp25Price && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= hp25Price;
            if (healthPlayer.currentHealth + 25 > 100)
            {
                healthPlayer.currentHealth = 100;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
            else
            {
                healthPlayer.currentHealth += 25;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
        }
    }
    
    public void Buy50Hp()
    {
        if (money.CurrentMoney >= hp50Price && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= hp50Price;
            if (healthPlayer.currentHealth + 50 > 100)
            {
                healthPlayer.currentHealth = 100;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
            else
            {
                healthPlayer.currentHealth += 50;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
        }
    }
    
    public void Buy75Hp()
    {
        if (money.CurrentMoney >= hp75Price && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= hp75Price;
            if (healthPlayer.currentHealth + 75 > 100)
            {
                healthPlayer.currentHealth = 100;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
            else
            {
                healthPlayer.currentHealth += 75;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
        }
    }
    
    public void Buy100Hp()
    {
        if (money.CurrentMoney >= hp100Price && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= hp100Price;
            if (healthPlayer.currentHealth + 100 > 100)
            {
                healthPlayer.currentHealth = 100;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
            else
            {
                healthPlayer.currentHealth += 100;
                healthBarMenu.SetHealth(healthPlayer.currentHealth);
            }
        }
    }
    
    
}
