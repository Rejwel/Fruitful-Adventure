
using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    private float _clampAngle = 80.0f;
    private float _xRot;
    private float _yRot;
    private float _xCurrRot;
    private float _yCurrRot;
    private float _xRotVelocity;
    private float _yRotVelocity;
    private float _smoothDampTime = 0.02f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        _xRot += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        _yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        _xRot = Mathf.Clamp(_xRot,-_clampAngle,_clampAngle);

        _xCurrRot = Mathf.SmoothDamp(_xCurrRot, _xRot, ref _xRotVelocity, _smoothDampTime);
        _yCurrRot = Mathf.SmoothDamp(_yCurrRot, _yRot, ref _yRotVelocity, _smoothDampTime);

        camera.transform.rotation = Quaternion.Euler(_xCurrRot,_yCurrRot,0f);
        player.transform.rotation = Quaternion.Euler(0f,_yCurrRot,0f);
    }
}
