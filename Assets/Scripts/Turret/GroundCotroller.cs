using System.Linq;
using UnityEngine;


public class GroundCotroller : MonoBehaviour
{

    [SerializeField] private GameObject[] placeableObjectPrefabs;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private GameObject currentPlaceableObject;
    [SerializeField] private GameObject player;
    [SerializeField] private float mouseWheelRotation;
    [SerializeField] private bool Menu = false;
    [SerializeField] private Inventory inv;
    [SerializeField] private uint[] placeableObjectPrefabsCount;
    [SerializeField] private GameObject shopIndicator;
    [SerializeField] private bool isShop = false;
    
    //Ring Menu Controller
    [SerializeField] private RingMenu MainMenuInstance;
    [SerializeField] private RingMenu MainMenuPrefab;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Prefab;
    public ControllerMode Mode {  get; set; }

    private void Start()
    {
        SetMode(ControllerMode.Play);
        inv = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        Debug.Log($"current placable object: {currentPlaceableObject}");

        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
        
        if (Input.GetKeyDown(KeyCode.Tab) && !isShop)
        {     
            SetMode(ControllerMode.Menu);
            Prefab = null;
            Destroy(currentPlaceableObject);
        }
        if (Mode == ControllerMode.Build)
        {
            if(Input.GetMouseButtonDown(1))
            {
                SetMode(ControllerMode.Play);
                Destroy(currentPlaceableObject);
            }
            if (Prefab == null)
            {
                SetMode(ControllerMode.Play);
            }
        }
    }
    

    private void HandleNewObjectHotkey()         
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Mode == ControllerMode.Build)
        {          
            if (inv.GameObjDictionary["Turret"] > 0 && Prefab != null)
            {
                player.GetComponent<PlayerShoot>().HoldFire = true;
                if (currentPlaceableObject != null)
                {
                    Destroy(currentPlaceableObject);
                }
                if (Physics.Raycast(ray, out hitInfo, 15f, terrain))
                {
                    currentPlaceableObject = Instantiate(Prefab);
                }
            }
            else if (inv.GameObjDictionary["TurretDetecting"] > 0 && Prefab != null)
            {
                player.GetComponent<PlayerShoot>().HoldFire = true;

                if (currentPlaceableObject != null)
                {
                    Destroy(currentPlaceableObject);
                }
                if (Physics.Raycast(ray, out hitInfo, 15f, terrain))
                {
                    currentPlaceableObject = Instantiate(Prefab);
                }
            }
        }
    }
    
    public void SetMenu(bool menu)
    {
        Menu = menu;
    }
    public void SetShop(bool shop)
    {
        isShop = shop;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10f, terrain))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            Destroy(currentPlaceableObject);
        }
    }


    private void RotateFromMouseWheel()
    {
        currentPlaceableObject.transform.Rotate(Vector3.up, 0f);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    public void ReleaseIfClicked()  //We are in Build mode, we have the object in our hand, we place it LPM  
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo, 8f, terrain))
        {
            //     uint tmpCount = 0;
            //     string currentObj = string.Concat(currentPlaceableObject.ToString().TakeWhile(x => x != '('));
            //     
            //     int pos = currentObj.IndexOf("Transparent");  
            //     if (pos >= 0) {
            //         string afterFounder = currentObj.Remove(pos);
            //         currentObj = afterFounder;
            //     } 
            //    
            //     tmpCount = inv.GameObjDictionary[currentObj];
            //     inv.GameObjDictionary.Remove(currentObj);
            //
            //     if (Prefab.name.Equals("Turret"))
            //     {
            //         Prefab = placeableObjectPrefabs[0];
            //         currentPlaceableObject = placeableObjectPrefabs[0];
            //         inv.RemoveShootingTurret();
            //     }
            //     else if (Prefab.name.Equals("TurretDetecting"))
            //     {
            //         Prefab = placeableObjectPrefabs[1];
            //         currentPlaceableObject = placeableObjectPrefabs[1];
            //         inv.RemoveDetectingTurret();
            //     }
            //     
            //     inv.GameObjDictionary.Add(currentObj, --tmpCount);
            //     currentPlaceableObject = null; 
            //     if(tmpCount == 0)
            //     { 
            //         Prefab = null;
            //     }
            //     player.GetComponent<PlayerShoot>().AddDelay();
            //     player.GetComponent<PlayerShoot>().HoldFire = false;
            // }
            // Prefab = placeableObjectPrefabs[0];
            // currentPlaceableObject = Prefab;
            Prefab = placeableObjectPrefabs[0];
            currentPlaceableObject = null;
            
            
            // this is associated with ring menu
            Prefab = null;
        }
    }
    

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;
        if (mode != ControllerMode.Menu && MainMenuInstance != null)
            Destroy(MainMenuInstance);

        switch (mode)
        {
            case ControllerMode.Build:
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();             
                break;
            case ControllerMode.Menu:
                Canvas.SetActive(true);               
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Camera.main.GetComponent<MouseLook>().enabled = false;
                player.GetComponent<PlayerShoot>().HoldFire = true;
                player.GetComponent<PlayerShoot>().AddDelay();
                break;
            case ControllerMode.Play:
                Canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                player.GetComponent<PlayerShoot>().HoldFire = false;
                break;            
        }
    }
    
    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }


    public void SetPrefab(int number)
    {
        Prefab = placeableObjectPrefabs[number]; 
    }

}