
using UnityEngine;

public class MouseLook : MonoBehaviour
{
     public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    private float mouseX;

    private float mouseY;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public float playerRotation()
    {
        return mouseX;
    }
}
