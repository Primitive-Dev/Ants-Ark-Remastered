using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 8f;
    public float jumpDuration = 0.25f;
    public float gravity = 20f;
    public float rotateSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rigidBody;
    private float jumpStartTime = 0f;
    private bool isJumping = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        moveDirection = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0)) * direction;

        if (direction.magnitude > 0)
        {
            rigidBody.velocity = moveDirection * speed;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpStartTime = Time.time;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (isJumping)
        {
            float jumpTime = Time.time - jumpStartTime;
            if (jumpTime >= jumpDuration || !Input.GetButton("Jump"))
            {
                isJumping = false;
            }
        }

        rigidBody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
    }

    private bool IsGrounded()
    {
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}
