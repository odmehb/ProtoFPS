using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float gravity = 9.81f;
    public float speed = 12f;
    public float jumpHeight = 10;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private static Vector3 moveTo;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        GroundCheck();

        HandleJump();

        Movement();
    }

    void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
        if (moveTo.magnitude == 0)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            var playerTransform = GetComponentInParent<Transform>();
            playerTransform.SetPositionAndRotation(moveTo, playerTransform.rotation);
            moveTo = default;
        }
    }

    public static void SetCharacterPosition(Vector3 position)
    {
        moveTo = position;
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
