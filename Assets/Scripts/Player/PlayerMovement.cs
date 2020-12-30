using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private float x;
    private float z;
    
    // for double jump
    private int jumps = 0;
    private bool checkJump = false;
    
    //for dash
    public Image dashImage;
    public ParticleSystem forwardDashParticle;
    public ParticleSystem backwardDashParticle;
    public ParticleSystem leftDashParticle;
    public ParticleSystem rightDashParticle;
    public ParticleSystem dJumpParticle;
    private float buttonCd = 0.5f;
    private int buttonCount = 0;
    KeyCode CurrKey;
    private float dashCounter = 0f;
    private float dashTime = 0.1f;
    private float dashStrength = 800f;
    private float dashCd = 5f;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Inventory inventory;

    Vector3 velocity;
    public Vector3 move;
    bool isGrounded;
    bool doubleJumpParticleOn = true;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        Dash();
        ResetJump();
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (knockBackCounter <= 0)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (jumps == 1 && doubleJumpParticleOn)
            {
                dJumpParticle.Play();
                doubleJumpParticleOn = false;
            }
            if (Input.GetButtonDown("Jump") && inventory.CanDoubleJump() && jumps < 1)
            {
                checkJump = false;
                jumps++;

                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (Input.GetButtonDown("Jump") && isGrounded)
            {
                checkJump = false;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            

            move = transform.right * x + transform.forward * z;
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
        
        controller.Move(move * speed * Time.deltaTime);
        
        //jumping
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        move = direction * knockBackForce;
        move.y = knockBackForce/3;
    }
    
    private void ResetJump()
    {
        if (isGrounded && checkJump == false)
        {
            jumps = 0;
            checkJump = true;
            doubleJumpParticleOn = true;
        }
    }
    
    private void Dash()
    {
        if (dashCounter > 0)
        {
            dashImage.fillAmount += 1f / dashCd * Time.deltaTime;
            dashCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CurrKey != KeyCode.W) buttonCount = 0;
                CurrKey = KeyCode.W;
                
                if (buttonCd > 0 && buttonCount == 1)
                {
                    dashImage.fillAmount = 0;
                    dashCounter = dashCd;
                    forwardDashParticle.Play();
                    controller.Move(transform.forward * dashStrength * Time.deltaTime);
                }
                else if (CurrKey == KeyCode.W)
                {
                    buttonCount += 1;
                    buttonCd = 0.3f;
                }
            }
    
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (CurrKey != KeyCode.A) buttonCount = 0;
                CurrKey = KeyCode.A;
                
                if (buttonCd > 0 && buttonCount == 1)
                {
                    dashImage.fillAmount = 0;
                    dashCounter = dashCd;
                    leftDashParticle.Play();
                    controller.Move(-transform.right * dashStrength * Time.deltaTime);
                }
                else if (CurrKey == KeyCode.A)
                {
                    buttonCount += 1;
                    buttonCd = 0.3f;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (CurrKey != KeyCode.S) buttonCount = 0;
                CurrKey = KeyCode.S;
                
                if (buttonCd > 0 && buttonCount == 1)
                {
                    dashImage.fillAmount = 0;
                    dashCounter = dashCd;
                    backwardDashParticle.Play();
                    controller.Move(-transform.forward * dashStrength * Time.deltaTime);
                }
                else if (CurrKey == KeyCode.S)
                {
                    buttonCount += 1;
                    buttonCd = 0.3f;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (CurrKey != KeyCode.D) buttonCount = 0;
                CurrKey = KeyCode.D;
                
                if (buttonCd > 0 && buttonCount == 1)
                {
                    dashImage.fillAmount = 0;
                    dashCounter = dashCd;
                    rightDashParticle.Play();
                    controller.Move(transform.right * dashStrength * Time.deltaTime);
                }
                else if (CurrKey == KeyCode.D)
                {
                    buttonCount += 1;
                    buttonCd = 0.3f;
                }
            }
    
            if (buttonCd > 0)
            {
                buttonCd -= 1 * Time.deltaTime;
            }
            else
            {
                buttonCount = 0;
            }
        }
    }
}
