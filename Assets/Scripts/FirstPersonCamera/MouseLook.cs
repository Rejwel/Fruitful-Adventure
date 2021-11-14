
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float clampAngle = 80.0f;
    private float xRotation;
    private float yRotation;
    private float mouseX;
    private float mouseY;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
 
        yRotation += mouseX * mouseSensitivity * Time.deltaTime;
        xRotation += mouseY * mouseSensitivity * Time.deltaTime;
 
        xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);
        
        Quaternion localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
        transform.rotation = localRotation;
        playerBody.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }
}
