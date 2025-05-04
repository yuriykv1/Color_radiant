using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootDelay = 0.3f;

    private bool isShooting = false;
    private bool canShoot = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    float SF = 0;
    private void Update()
    {


        if (animator.GetBool("shoot")) 
        {
            if (SF <= 1f)
            {
                SF = SF + 0.05f;
            }
            animator.SetFloat("shootfloat" , SF);
        }
        else 
        {
            if (SF >= 0f)
            {
                SF = SF - 0.05f;
            }
            animator.SetFloat("shootfloat", SF);
        }
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("shoot"))
        {
            StartCoroutine(Shoot());
            

        }



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
