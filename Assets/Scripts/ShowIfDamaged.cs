using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfDamaged : MonoBehaviour
{
    public GameObject Building;
    public GameObject Icon;
    public float HP;
    public BuildingHealth Health;
    private static bool info=true;
    private static bool check=true;

    // Start is called before the first frame update
    void Awake()
    {
        Icon.SetActive(false);
    }

    private void Update()
    {
        if (info)
        {
            setHP();
        }

        if (check)
        {
            if (HP != Health.currentHealth)
            {
                check = false;
                StartCoroutine(Waiter());
            }
            else if (HP == Health.currentHealth)
            {
                hideIcon();
            }
        }
    }

    public void setHP()
    {
        Health = Building.GetComponent<BuildingHealth>();
        HP = Health.currentHealth;
        info = false;
    }

    public void showIcon()
    {
        HP = Health.currentHealth;
    }

    public void hideIcon()
    {
        Icon.SetActive(false);
    }

    private IEnumerator Waiter()
    {
        Icon.SetActive(true);
        yield return new WaitForSeconds(2);
        showIcon();
        yield return new WaitForSeconds(1);
        check = true;
    }
}
