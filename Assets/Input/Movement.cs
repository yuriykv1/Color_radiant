using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    private bool isGrounded;
    private float xRotation = 0f;
    public float runSpeed = 10f;
    public float walkSpeed = 5f;
    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        HandleMouseLook();  
        HandleMovement();
        ShiftDown();
       
    }
    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }


        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        Vector3 move = transform.right * x + transform.forward * z;

        float forward = Vector3.Dot(transform.forward, move);
        animator.SetFloat("forward", forward);

        controller.Move(move * speed * Time.deltaTime);
         
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    public void ShiftDown()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            if (speed >= runSpeed)
            {
                speed = runSpeed;
            }
            else { speed += 1 / 5; }

        }
        if (isRunning == false)
        {

            if (speed <= walkSpeed)
            {
                speed = walkSpeed;
            }
            else { speed -= 1 / 5; }

        }
        Debug.Log("Speed:" + speed);
    }
}
