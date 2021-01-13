using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class ShopGui : MonoBehaviour
{
    // ammo * 10 price
    private int shotgunPrice = 200;
    private int riflePrice = 300;
    private int minigunPrice = 400;
    private int shotgunAmmoPrice = 1;
    private int rifleAmmoPrice = 1;
    private int minigunAmmoPrice = 1;
    private int dashPrice = 200;
    private int dJumpPrice = 200;
    private int ShieldPrice = 50;
    private int hp25Price = 50;
    private int hp50Price = 90;
    private int hp75Price = 130;
    private int hp100Price = 160;

    private TextMeshProUGUI currMoney;
    private TextMeshProUGUI currPistolAmmo;
    private TextMeshProUGUI currShotgunAmmo;
    private TextMeshProUGUI currRifleAmmo;
    private TextMeshProUGUI currMinigunAmmo;

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

        dash = GameObject.Find("BuyDash");
        dJump = GameObject.Find("BuyDoubleJump");
        shield = GameObject.Find("BuyShield");

        shotgun = GameObject.Find("BuyShotgun");
        rifle = GameObject.Find("BuyRifle");
        minigun = GameObject.Find("BuyMinigun");
        
        money = FindObjectOfType<Money>();
        money.CurrentMoney += 1000;
    }
    
    void Update()
    {
        // for shield
        if (inv.isShielded())
        {
            shield.GetComponent<Button>().interactable = false;
            shield.GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        else
        {
            shield.GetComponent<Button>().interactable = true;
            shield.GetComponentsInChildren<Image>()[1].color = Color.white;
        }
        
        // for dash
        if (inv.CanDash())
        {
            dash.GetComponent<Button>().interactable = false;
            dash.GetComponentsInChildren<Image>()[1].color = Color.green;
        }

        // for dJump
        if(inv.CanDoubleJump())
        {
            dJump.GetComponent<Button>().interactable = false;
            dJump.GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        
        // for weaponShotgun
        if(inv.currentGuns.Exists(x => x.GetId() == 1))
        {
            shotgun.GetComponent<Button>().interactable = false;
            shotgun.GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        
        // for weaponRifle
        if(inv.currentGuns.Exists(x => x.GetId() == 2))
        {
            rifle.GetComponent<Button>().interactable = false;
            rifle.GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        // for weaponMinigun
        if(inv.currentGuns.Exists(x => x.GetId() == 3))
        {
            minigun.GetComponent<Button>().interactable = false;
            minigun.GetComponentsInChildren<Image>()[1].color = Color.green;
        }

        // TEXT
        currMoney.text = $"Your current money {money.CurrentMoney} ■";

        if(inv.bulletAmmount[1] > 10000)
            currShotgunAmmo.text = "Current : ∞";
        else
            currShotgunAmmo.text = $"Current : {inv.bulletAmmount[1]}";
        
        if(inv.bulletAmmount[2] > 10000)
            currRifleAmmo.text = "Current : ∞";
        else
            currRifleAmmo.text = $"Current : {inv.bulletAmmount[2]}";
        
        if(inv.bulletAmmount[3] > 10000)
            currMinigunAmmo.text = "Current : ∞";
        else
            currMinigunAmmo.text = $"Current : {inv.bulletAmmount[3]}";

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
