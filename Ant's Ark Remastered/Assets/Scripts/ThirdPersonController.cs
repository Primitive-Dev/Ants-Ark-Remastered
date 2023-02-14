using System;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 6f;
    public float jumpForce = 8f;
    public float jumpDuration = 0.25f;
    //public float gravity = 20f;
    public float rotationSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] Rigidbody rigidbody;
    private float jumpStartTime = 0f;
    [SerializeField] bool isJumping = false;
    //[SerializeField] bool isGrounded;

    void FixedUpdate()
    {
        PlayerRotation();
        PlayerMovement();
        HandleJump();
        
    }

    private void PlayerRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        moveDirection = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0)) * direction;

        if (direction.magnitude > 0)
        {
            rigidbody.velocity = moveDirection * movementSpeed + Physics.gravity * Time.deltaTime;
        }
        else
        {
            rigidbody.velocity = Physics.gravity * Time.deltaTime;
        }
    }

    //private void PlayerMovement()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //    Vector3 direction = new Vector3(horizontal, 0, vertical);
    //    moveDirection = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0)) * direction;

    //    if (direction.magnitude > 0)
    //    {
    //        rigidbody.velocity = moveDirection * movementSpeed;
    //    }
    //    else
    //    {
    //        rigidbody.velocity = Vector3.zero;
    //    }
    //    //rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    //}


    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //    isJumping = true;
        //    jumpStartTime = Time.time;
        //    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}

        //if (isJumping)
        //{
        //    float jumpTime = Time.time - jumpStartTime;
        //    if (jumpTime >= jumpDuration || !Input.GetButton("Jump"))
        //    {
        //        isJumping = false;
        //    }
        //}
    }

    private bool IsGrounded()
    {
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}
