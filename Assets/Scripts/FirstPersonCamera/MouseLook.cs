using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [SerializeField] public float mouseSensitivity = 150f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    private float _clampAngle = 80.0f;
    private float _mouseX;
    private float _mouseY;
    private float _xRotation;

    private Quaternion _lookingCameraRotation;
    private Quaternion _lookingPlayerRotation;

    private bool _isLookingAt = false;
    private float _lookingTime = 3f;
    private float _lookingCounter = 0f;

    
    void Awake()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouse_sensitivity_slider_value");
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
            if (Time.timeScale != 0)
                PlayerMouseMove();
        }
    }

    private void PlayerMouseMove()
    {
        _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity / 150;
        _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity / 150;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation,-_clampAngle,_clampAngle);

        camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * _mouseX * mouseSensitivity / 150);
        
    }

    public void LookAtObject(GameObject focusPoint)
    {
        _xRotation = 0;
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
