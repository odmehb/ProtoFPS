using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float gravity;
    public float speed = 12f;
    public float jumpHeight = 10;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;


    private Vector3 velocity;
    private bool isGrounded;


    void Update()
    {
        GroundCheck();

        HandleJump();

        Movement();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.right * x + transform.forward * z;

        controller.Move(moveVector * speed * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //looks better if the character is forced to the ground
            velocity.y = -2f;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                // https://www.thehoopsgeek.com/the-physics-of-the-vertical-jump/
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
    }
}
