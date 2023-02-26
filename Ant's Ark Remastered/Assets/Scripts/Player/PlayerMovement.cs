using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform camera;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10f; // The speed at which the player rotates

    //[SerializeField] float turnSmoothTime;
    //float turnSmoothVelocity;
    

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    //[SerializeField] float airMultiplier = 0.4f;
    //[SerializeField] float movementMultiplier = 2f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    //[SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    [SerializeField] int fallMultiplier;
    [SerializeField] int lowJumpMultiplier;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    //[SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 2f;
    [SerializeField] float airDrag = 0f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Vector3 cameraForward;
    Vector3 cameraRight;

    Rigidbody rb;

    RaycastHit slopeHit;


    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        MyInput();

        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    //SHOWS GROUND CHECK AREA
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //Movement Relative to camera. Ignores up/down movement if camera is looking up/down
        cameraForward = camera.transform.forward;
        cameraRight = camera.transform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;


        moveDirection = cameraForward * verticalMovement + cameraRight * horizontalMovement;
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }


    //COMPARE WITH SPEEDCONTROL FROM OLDPLAYERMOVEMENT SCRIPT FOR SNAPPYIER MOVEMENT
    void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        //if(isGrounded && horizontalMovement > 0 || verticalMovement > 0)
        //{
        //    return;
        //}
        //else
        //{
        //    rb.velocity = Vector3.zero;
        //}

        //if (Input.GetKey(sprintKey) && isGrounded)
        //{
        //    moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        //}
        //else
        //{
        //    moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        //}
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        HandleRotation();
        BetterJump();
        MovePlayer();
    }

    void HandleRotation()
    {
        ////Handle Rotation

        cameraForward.y = 0f; // Set y component to 0 to prevent tilting
        Quaternion desiredRotation = Quaternion.LookRotation(cameraForward);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized, ForceMode.Impulse);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized, ForceMode.Impulse);
        }
        else if (!isGrounded)
        {
            //rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Impulse);
            rb.AddForce(moveDirection.normalized, ForceMode.Impulse);
        }
    }
}