using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [Header("Misc")] 
    public Transform cameraTransform;
    public Transform firePoint;
    [SerializeField] private Transform gunPosition;
    
    public GameObject [] BulletObjects;
    private int NumberOfBullet { get; set; }
    public GameObject GunPrefab;
    public bool HoldFire { get; set; }
    private Inventory inventory;
    
    private Gun currentGun;
    private int currentGunIndex;

    public Vector3 upRecoil;
    public Vector3 bulletOffset;
    private Vector3 originalRotationOfFirepoint;
    private float bulletSpread;
    private float bulletSpeed = 500f;
    private int[] magazine = new int[5];
    private int stashAmmo;
    private int bullets;
    private string desc;
    private bool firstEmptyBullet;
    private bool reloading = false;

    [Header("Timers")]
    private float fireRate;
    private float emptyTime = 0.8f;
    private float reloadTime = 2f;
    private float nextTimeToFire { get; set; }
    private float nextTimeToEmpty { get; set; }
    private float nextTimeToReload { get; set; }
    
    [Header("TMP")]
    private TextMeshProUGUI  currentGunText;
    private TextMeshProUGUI currentAmmoText;
    private TextMeshProUGUI  currentStashAmmoText;
    
    private void Start()
    {
        inventory = GetComponent<Inventory>();
        SetStartingAmmo();
        
        HoldFire = false;
        originalRotationOfFirepoint = firePoint.transform.localEulerAngles;
        firstEmptyBullet = true;
        currentStashAmmoText = GameObject.Find("StashAmmo").GetComponent<TextMeshProUGUI>();
        currentGunText = GameObject.Find("CurrentGunText").GetComponent<TextMeshProUGUI>();
        currentAmmoText = GameObject.Find("Magazine").GetComponent<TextMeshProUGUI>();

        currentGun = inventory.currentGuns[0];
        currentGunIndex = currentGun.GetId();
        ChangeGun(currentGun);
    }

    void Update()
    {
        ShootingLogic();
        Reloading();
        ChangeGunFromScrollInput();
        ChangeGunFromKeyboardInput();
    }

    private void ShootingLogic()
    {
        if(!HoldFire){
            GunPrefab.SetActive(true);
            if (Input.GetButtonDown("Fire1")) ReturnToOriginalRecoil();
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazine[currentGun.GetId()] > 0)
            {
                Shoot();
            }
            else if (magazine[currentGun.GetId()] == 0 && firstEmptyBullet)
            {
                firstEmptyBullet = false;
                nextTimeToEmpty = Time.time + emptyTime;
            }
            else if (Input.GetButton("Fire1") && magazine[currentGun.GetId()] == 0 && Time.time >= nextTimeToEmpty)
            {
                if (currentGun.GetDesc().Equals("Pistol") && !reloading)
                {
                    Empty("Pistol_empty");
                }
                else if (currentGun.GetDesc() != "Pistol" && !reloading)
                {
                    Empty("Gun_empty");
                }
            }
        }
        else
        {
            GunPrefab.SetActive(false);
        }
    }

    private void Reloading()
    {
        if (reloading && Time.time >= nextTimeToFire)
        {
            LowerStashAmmo(currentGun);
            reloading = false;
        }
        
        if (Input.GetKey(KeyCode.R) && Time.time >= nextTimeToReload && inventory.bulletAmmount[currentGun.GetId()] > 0 && magazine[currentGun.GetId()] != currentGun.GetMagazine())
        {
            Reload(currentGun);
        }
    }

    private void ChangeGunFromScrollInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            currentGunIndex += (Convert.ToInt32(Input.GetAxis("Mouse ScrollWheel") * 10));
            if (currentGunIndex < 0)
            {
                currentGunIndex = 0;
            }
            else if (currentGunIndex > inventory.currentGuns.Count-1)
            {
                currentGunIndex = inventory.currentGuns.Count-1;
            }
            currentGun.GetGunModel().SetActive(false);
            currentGun = inventory.currentGuns[currentGunIndex];
            ChangeGun(currentGun);
        }
    }

    private void ChangeGunFromKeyboardInput()
    {
        if (Input.GetKeyDown("1"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(0) != null)
            {
                currentGun.GetGunModel().SetActive(false);
                currentGun = inventory.currentGuns[0];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(1) != null)
            {
                currentGun.GetGunModel().SetActive(false);
                currentGun = inventory.currentGuns[1];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(2) != null)
            {
                currentGun.GetGunModel().SetActive(false);
                currentGun = inventory.currentGuns[2];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("4"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(3) != null)
            {
                currentGun.GetGunModel().SetActive(false);
                currentGun = inventory.currentGuns[3];
                ChangeGun(currentGun);
            }
        }
        if (Input.GetKeyDown("5"))
        {
            if (inventory.currentGuns.ElementAtOrDefault(4) != null)
            {
                currentGun.GetGunModel().SetActive(false);
                currentGun = inventory.currentGuns[4];
                ChangeGun(currentGun);
            }
        }
    }

    void Shoot()
    {
        AddRecoil();
        magazine[currentGun.GetId()]--;
        currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
        fireRate = currentGun.GetFireRate();
        bullets = currentGun.GetBullets();
        desc = currentGun.GetDesc();
        nextTimeToFire = Time.time + fireRate;
        GameObject InstantiateBullet;
        AudioManager.playSound(desc);

        for (int i = 0; i < bullets; i++)
        {
            Vector3 forwardVector = Vector3.forward;
            
            float deviation = Random.Range(0f, bulletSpread);
            float angle = Random.Range(0f, 360f);
            
            forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
            forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
            forwardVector = firePoint.transform.rotation * forwardVector;

            // for minigun
            if (currentGun.GetId() == 3)
            {
                InstantiateBullet = Instantiate(BulletObjects[NumberOfBullet++], firePoint.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90,0)));
                InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
                if (NumberOfBullet == BulletObjects.Length) NumberOfBullet = 0;
            }
            // for pistol
            else if (currentGun.GetId() == 0)
            {
                InstantiateBullet = Instantiate(BulletObjects[5], firePoint.position, transform.rotation  * Quaternion.Euler(new Vector3(0, 90,0)));
                InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
            }
            // for rifle
            else if (currentGun.GetId() == 2)
            {
                InstantiateBullet = Instantiate(BulletObjects[1], firePoint.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90,0)));
                InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
            }
            // for shotgun
            else if (currentGun.GetId() == 1)
            {
                InstantiateBullet = Instantiate(BulletObjects[6], firePoint.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90,0)));
                InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
            }
            // for sniper
            else if (currentGun.GetId() == 4)
            {
                InstantiateBullet = Instantiate(BulletObjects[1], firePoint.position, transform.rotation * Quaternion.Euler(new Vector3(0, 90,0)));
                InstantiateBullet.GetComponent<Rigidbody>().AddForce(forwardVector * bulletSpeed);
            }
        }
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    private void ChangeGun(Gun currentGun)
    {
        // AudioManager.stopSound("Gun_reload");
        reloading = false;
        
        currentGun.GetGunModel().SetActive(true);
        
        firstEmptyBullet = true;
        fireRate = currentGun.GetFireRate();
        bulletSpeed = currentGun.GetSpeed();
        bulletSpread = currentGun.GetSpread();

        if(inventory.bulletAmmount[currentGun.GetId()] > 100000)
        {
            currentStashAmmoText.text = "∞";
        }
        else
        {
            currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
        }

        currentAmmoText.text = currentGun.GetMagazine().ToString();
        
        currentGunText.text = "Gun: " + currentGun.GetDesc();
        currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
    }

    private void Empty(string sound)
    {
        nextTimeToEmpty = Time.time + emptyTime;
        AudioManager.playSound(sound);
    }

    private void Reload(Gun currentGun)
    {
        reloading = true;
        nextTimeToReload = Time.time + reloadTime;
        firstEmptyBullet = true;
        AudioManager.playSound("Gun_reload");
        nextTimeToFire = Time.time + reloadTime;
    }

    private void LowerStashAmmo(Gun currentGun)
    {
        int ammoToSubstract;
        
        if (inventory.bulletAmmount[currentGun.GetId()] + magazine[currentGun.GetId()] <= currentGun.GetMagazine())
        {
            magazine[currentGun.GetId()] += inventory.bulletAmmount[currentGun.GetId()];
            inventory.bulletAmmount[currentGun.GetId()] = 0;
            currentAmmoText.text = "Ammo " + magazine[currentGun.GetId()];
        }
        else if (inventory.bulletAmmount[currentGun.GetId()] + magazine[currentGun.GetId()] > currentGun.GetMagazine())
        {
            ammoToSubstract = currentGun.GetMagazine() - magazine[currentGun.GetId()];
            magazine[currentGun.GetId()] = currentGun.GetMagazine();
            inventory.bulletAmmount[currentGun.GetId()] -= ammoToSubstract;
            currentAmmoText.text = "Ammo " + currentGun.GetMagazine();
        }
        if(inventory.bulletAmmount[currentGun.GetId()] > 10000)
        {
            currentStashAmmoText.text = "∞";
        }
        else
        {
            currentStashAmmoText.text = inventory.bulletAmmount[currentGun.GetId()].ToString();
        }
    }

    private void SetStartingAmmo()
    {
        for (int i = 0; i < GunContainer.guns.Count; i++)
        {
            magazine[i] = GunContainer.GetGun(i).GetMagazine();
        }
    }
    
    private void AddRecoil()
    {
        firePoint.transform.localEulerAngles += upRecoil;
    }

    private void ReturnToOriginalRecoil()
    {
        firePoint.transform.localEulerAngles = originalRotationOfFirepoint;
    }

    public void AddDelay()
    {
        nextTimeToFire = Time.time + 1f;
        nextTimeToEmpty = Time.time + emptyTime;
    }
}
