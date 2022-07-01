using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    [Header("PLayer Settings")]
    public float speed = 12f;
    public float sneakSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 10f;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float canJump = 0f;

    [Header("Leaning")]
    public Transform LeanPivot;
    private float currentLean;
    private float targetLean;
    public float leanAngle;
    public float leanSmoothing;
    private float leanVelocity;

    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Jump with delay
        if(Input.GetKey("space") && isGrounded && Time.time > canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = Time.time + 2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



        if (Input.GetKey("left shift") && isGrounded)
        {
            controller.Move(move * sneakSpeed * Time.deltaTime);
        } else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        //Leaning (i know its kinda ugly but it works)
        CalculateLeaning();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeanLeft(30);
        } else if (Input.GetKeyUp(KeyCode.Q))
        {
            LeanRight(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LeanRight(30);
        } else if (Input.GetKeyUp(KeyCode.E)) 
            {
            LeanLeft(0);
        }


    }
    private void LeanRight(float leanAngle)
    {
        targetLean = -leanAngle;
    }

    private void LeanLeft(float leanAngle)
    {
        targetLean = leanAngle;

    }
    private void CalculateLeaning()
    {
        currentLean = Mathf.SmoothDamp(currentLean, targetLean, ref leanVelocity, leanSmoothing);

        LeanPivot.localRotation = Quaternion.Euler(new Vector3(0, 0, currentLean));
    }
}
