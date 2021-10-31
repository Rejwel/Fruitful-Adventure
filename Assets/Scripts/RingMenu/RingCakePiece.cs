using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RingCakePiece : MonoBehaviour
{
    public Image Icon;
    public Image CakePiece;
    public TextMeshProUGUI Amount;
    private Inventory inv;

   private int currentAmountTurret = 0;

    void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        currentAmountTurret = (int)inv.GameObjDictionary["Turret"];
        // currentAmountDetectingTurret = (int)inv.GameObjDictionary["TurretDetecting"];
       Amount.text = currentAmountTurret.ToString();
        //Amount.text = currentAmountDetectingTurret.ToString();
    }
}