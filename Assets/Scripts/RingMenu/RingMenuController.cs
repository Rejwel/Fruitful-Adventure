using System;
using System.Collections;
using UnityEngine;

public class RingMenuController : MonoBehaviour
{
    
    private Inventory inv;
    public GameObject player;
    public string Sciezka;
    


    void Start()
    {
        
        inv = FindObjectOfType<Inventory>();   
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currItem);

            

        /*if (Mode == ControllerMode.Play)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetMode(ControllerMode.Build);
            }
        }
        else if (Mode == ControllerMode.Build || Mode == ControllerMode.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SetMode(ControllerMode.Play);
            }
        }
   
        */
    }

    

    
 

   
}

