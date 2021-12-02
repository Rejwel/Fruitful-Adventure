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
    private int sniperPrice = 250;
    
    private int shotgunAmmoPrice = 5;
    private int rifleAmmoPrice = 1;
    private int minigunAmmoPrice = 2;
    private int sniperAmmoPrice = 8;
    
    private int dashPrice = 200;
    private int dJumpPrice = 200;
    private int ShieldPrice = 50;
    
    private int hp25Price = 50;
    private int hp50Price = 90;
    private int hp75Price = 130;
    private int hp100Price = 160;

    private int GrenadePrice = 75;
    private int FencePrice = 75;
    private int ShootingTurretPrice = 75;
    private int SlowingTurretPrice = 50;
    
    private TextMeshProUGUI currMoney;
    private TextMeshProUGUI currPistolAmmo;
    private TextMeshProUGUI currShotgunAmmo;
    private TextMeshProUGUI currRifleAmmo;
    private TextMeshProUGUI currMinigunAmmo;
    private TextMeshProUGUI currSniperAmmo;
    private TextMeshProUGUI currGrenades;
    private TextMeshProUGUI currShootingTurrets;
    private TextMeshProUGUI currSlowingTurrets;
    private TextMeshProUGUI currFences;
    private ColorBlock GreenColorDisabled;

    private GameObject dash;
    private GameObject dJump;
    private GameObject shield;

    private GameObject shotgun;
    private GameObject rifle;
    private GameObject minigun;
    private GameObject sniper;

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
        currSniperAmmo = GameObject.Find("CurrSniperAmmo").GetComponent<TextMeshProUGUI>();
        
        currGrenades = GameObject.Find("CurrGrenades").GetComponent<TextMeshProUGUI>();
        currShootingTurrets = GameObject.Find("CurrShootingTurrets").GetComponent<TextMeshProUGUI>();
        currSlowingTurrets = GameObject.Find("CurrSlowingTurrets").GetComponent<TextMeshProUGUI>();
        currFences = GameObject.Find("CurrFences").GetComponent<TextMeshProUGUI>();

        dash = GameObject.Find("BuyDash");
        dJump = GameObject.Find("BuyDoubleJump");
        shield = GameObject.Find("BuyShield");

        shotgun = GameObject.Find("BuyShotgun");
        rifle = GameObject.Find("BuyRifle");
        minigun = GameObject.Find("BuyMinigun");
        sniper = GameObject.Find("BuySniper");
        
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
        // for weaponSniper
        if(inv.currentGuns.Exists(x => x.GetId() == 4))
        {
            sniper.GetComponent<Button>().interactable = false;
        }

        // TEXT
        currMoney.text = $"= {money.CurrentMoney}";
        currGrenades.text = $"({GrenadePrice}) Grenade {inv.GetGrenades()}";
        currShootingTurrets.text = $"({ShootingTurretPrice}) Turret {inv.GetShootingTurret()}";
        currSlowingTurrets.text = $"({SlowingTurretPrice}) Slowing Turret {inv.GetSlowingTurret()}";
        currFences.text = $"({FencePrice}) Fence {inv.GetTrapFence()}";
        

        if(inv.bulletAmmount[1] > 10000)
            currShotgunAmmo.text = $"Shotgun ammo\n({shotgunAmmoPrice*10}) Current : ∞";
        else
            currShotgunAmmo.text = $"Shotgun ammo\n({shotgunAmmoPrice*10}) Current : {inv.bulletAmmount[1]}";
        
        if(inv.bulletAmmount[2] > 10000)
            currRifleAmmo.text = $"Rifle ammo\n({rifleAmmoPrice*10}) Current : ∞";
        else
            currRifleAmmo.text = $"Rifle ammo\n({rifleAmmoPrice*10}) Current : {inv.bulletAmmount[2]}";
        
        if(inv.bulletAmmount[3] > 10000)
            currMinigunAmmo.text = $"Minigun ammo\n({minigunAmmoPrice*10}) Current : ∞";
        else
            currMinigunAmmo.text = $"Minigun ammo\n({minigunAmmoPrice*10}) Current : {inv.bulletAmmount[3]}";
        
        if(inv.bulletAmmount[4] > 10000)
            currSniperAmmo.text = $"Sniper ammo\n({sniperAmmoPrice*10}) Current : ∞";
        else
            currSniperAmmo.text = $"Sniper ammo\n({sniperAmmoPrice*10}) Current : {inv.bulletAmmount[4]}";

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
    
    public void BuySniper()
    {
        if (money.CurrentMoney >= sniperPrice)
        {
            money.CurrentMoney -= sniperPrice;
            inv.AddSniper();
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
    
    public void BuyFence()
    {
        if (money.CurrentMoney >= FencePrice)
        {
            money.CurrentMoney -= FencePrice;
            inv.AddTrapFence();
        }
    }
    
    public void BuyShootingTurret()
    {
        if (money.CurrentMoney >= ShootingTurretPrice)
        {
            money.CurrentMoney -= ShootingTurretPrice;
            inv.AddShootingTurret();
        }
    }
    
    public void BuyDetectingTurret()
    {
        if (money.CurrentMoney >= SlowingTurretPrice)
        {
            money.CurrentMoney -= SlowingTurretPrice;
            inv.AddSlowingTurret();
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
    
    public void BuySniperAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * sniperAmmoPrice)
        {
            money.CurrentMoney -= ammo * sniperAmmoPrice;
            inv.bulletAmmount[4] += ammo;
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
