using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public Vector3 move;
    bool isGrounded;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;



    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (knockBackCounter <= 0)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            move = transform.right * x + transform.forward * z;
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
        
        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        move = direction * knockBackForce;
        move.y = knockBackForce/3;
    }
    
}
