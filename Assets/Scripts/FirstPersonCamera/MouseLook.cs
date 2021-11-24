
using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 150f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    private float _clampAngle = 80.0f;
    private float mouseX;
    private float mouseY;
    private float xRotation;

    private Quaternion _lookingCameraRotation;
    private Quaternion _lookingPlayerRotation;

    private bool _isLookingAt = false;
    private float _lookingTime = 3f;
    private float _lookingCounter = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        
        if (_isLookingAt)
        {
            if (_lookingCounter >= _lookingTime) CancelLookingAtObject();
            _lookingTime += Time.time;
        }
        else
        {
            PlayerMouseMove();
        }
    }

    private void PlayerMouseMove()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-_clampAngle,_clampAngle);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX * mouseSensitivity/2 * Time.deltaTime);
        
    }

    public void LookAtObject(GameObject focusPoint)
    {
        xRotation = 0;
        _lookingCounter += Time.time + _lookingTime;
        _isLookingAt = true;

        camera.transform.localRotation = Quaternion.Lerp(camera.transform.localRotation, Quaternion.Euler(0,0,0), 6f * Time.deltaTime);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, focusPoint.transform.rotation, 3f * Time.deltaTime);
    }

    public void CancelLookingAtObject()
    {
        _isLookingAt = false;
    }
}
