using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;
    public Rigidbody rb;
    public float jumpForce = 5f;
    private float xRotation = 0f;
    private bool isGrounded;
    private bool isNearWall;
    void Update()
    {
        RotatePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        Vector3 velocity = direction * speed;

        if (isNearWall && horizontal != 0)
        {
            velocity.x = 0;
        }

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
