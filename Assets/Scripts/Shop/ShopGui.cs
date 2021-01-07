using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class ShopGui : MonoBehaviour
{
    private TextMeshProUGUI currMoney;
    private TextMeshProUGUI currPistolAmmo;
    private TextMeshProUGUI currShotgunAmmo;
    private TextMeshProUGUI currRifleAmmo;
    private TextMeshProUGUI currMinigunAmmo;

    private GameObject dash;
    private GameObject dJump;
    private GameObject shield;

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
        currPistolAmmo = GameObject.Find("CurrPistolAmmo").GetComponent<TextMeshProUGUI>();
        currShotgunAmmo = GameObject.Find("CurrShotgunAmmo").GetComponent<TextMeshProUGUI>();
        currRifleAmmo = GameObject.Find("CurrRifleAmmo").GetComponent<TextMeshProUGUI>();
        currMinigunAmmo = GameObject.Find("CurrMinigunAmmo").GetComponent<TextMeshProUGUI>();

        dash = GameObject.Find("BuyDash");
        dJump = GameObject.Find("BuyDoubleJump");
        shield = GameObject.Find("BuyShield");
        
        
        money = GameObject.FindObjectOfType<Money>();
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


        // TEXT
        currMoney.text = $"Your current money {money.CurrentMoney} ■";
        
        if(inv.bulletAmmount[0] > 10000)
            currPistolAmmo.text = "Current : ∞";
        else
            currPistolAmmo.text = $"Current : {inv.bulletAmmount[0]}";
        
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

    public void changeRed(Image img)
    {
        img.color = Color.red;
    }

    public void changeDefault(Image img)
    {
        img.color = Color.white;
    }


    public void BuyDoubleJump()
    {
        if (money.CurrentMoney >= 500)
        {
            money.CurrentMoney -= 500;
            inv.activeDoubleJump();
        }
    }
    
    public void BuyDash()
    {
        if (money.CurrentMoney >= 500)
        {
            money.CurrentMoney -= 500;
            inv.activeDash();
        }
    }
    
    public void BuyShiled()
    {
        if (money.CurrentMoney >= 50)
        {
            money.CurrentMoney -= 50;
            inv.activeShield();
        }
    }

    public void BuyPistolAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * 1)
        {
            money.CurrentMoney -= ammo * 1;
            inv.bulletAmmount[0] += ammo;
        }
    }
    
    public void BuyShotgunAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * 1)
        {
            money.CurrentMoney -= ammo * 1;
            inv.bulletAmmount[1] += ammo;
        }
    }
    
    public void BuyRifleAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * 1)
        {
            money.CurrentMoney -= ammo * 1;
            inv.bulletAmmount[2] += ammo;
        }
    }
    
    public void BuyMinigunAmmo(int ammo = 10)
    {
        if (money.CurrentMoney >= ammo * 1)
        {
            money.CurrentMoney -= ammo * 1;
            inv.bulletAmmount[3] += ammo;
        }
    }

    public void Buy25Hp()
    {
        if (money.CurrentMoney >= 50 && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= 50;
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
        if (money.CurrentMoney >= 90 && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= 90;
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
        if (money.CurrentMoney >= 130 && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= 130;
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
        if (money.CurrentMoney >= 160 && healthPlayer.currentHealth < 100)
        {
            money.CurrentMoney -= 160;
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
