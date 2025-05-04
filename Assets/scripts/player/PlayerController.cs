using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;

    public float rotationSpeed = 13f;
    public float walkSpeed = 1f;
    public float runSpeed = 7f;
    public float horizontalRunSpeed = 4f;
    public float speedSmoothTimeStanding = 0.05f;
    public float speedSmoothTimeMoving = 0.2f;

    private float currentSpeed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootDelay = 0.3f;

    private bool isShooting = false;
    private bool canShoot = true;
    private float speedVelocity;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;
    public Rigidbody rb;
    public float jumpForce = 5f;
    private float xRotation = 0f;
    private bool isGrounded;
    private bool isNearWall;
    public float speed = 1f;
   // ////void Start()
   // {
   //     animator = GetComponent<Animator>();
   //     rigidbody = GetComponent<Rigidbody>();
   ///// <summa}
   ///// </summary>
    private void Update()
    {
        RotatePlayer();
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    Jump();
        //}
    }

    private void FixedUpdate()
    {
        //   float move = Input.GetAxis("Vertical");  // (W/S) 
        //   float horizontalMove = Input.GetAxis("Horizontal");  // (A/D) 
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //if (isRunning) 
        //{
            
        //    if (speed >= runSpeed)
        //    {
        //        speed = runSpeed;
        //    }
        //    else {speed += 1/5; }

        //}
        //if (isRunning == false)
        //{

        //    if (speed <= walkSpeed)
        //    {
        //        speed = walkSpeed;
        //    }
        //    else { speed -= 1 / 5; }

        //}
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        //Vector3 velocity = direction * speed;

       // if (isNearWall && horizontal != 0)
      //  {
        //    velocity.x = 0;
       // }

       // rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        //    move = Mathf.Clamp(move, 0, 1);
        //    float targetSpeed = /*Mathf.Abs(move)*/ move * (isRunning ? runSpeed : walkSpeed);
        // horizontalMove = Mathf.Clamp(horizontalMove, 0, 1);


        // smooth speed
        //   float smoothTime = currentSpeed < 0.1f ? speedSmoothTimeStanding : speedSmoothTimeMoving;
        //  currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothTime);
        //currentSpeed = 5f;
        // move


        //rigidbody.linearVelocity = new Vector3 (direction.x, rigidbody.linearVelocity.y,direction.z ) * currentSpeed * Time.deltaTime;

        // animate
        //animator.SetFloat("speed", currentSpeed);
        //if (Input.GetMouseButton(0) && !animator.GetBool("shoot"))
        //{
        //    animator.SetBool("shoot", true);
        //}
        //else if (!Input.GetMouseButton(0) && animator.GetBool("shoot"))
        //{
        //    animator.SetBool("shoot", false);
        //}
        //if (Input.GetMouseButtonDown(0)) { StartCoroutine(Shoot()); }


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

    private IEnumerator Shoot()
    {
        canShoot = false;
        animator.SetTrigger("Shooting");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit object: " + hit.collider.name);
            Debug.DrawLine(firePoint.position, hit.point, Color.green, 7f);
        }
        else
        {
            Debug.Log("Missed");
        }

        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
