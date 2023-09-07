using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private Transform firePoint;
    private Camera cam;
    public Animator animator;
    private bool IsAttacking = false;
    private bool IsPunching = false;
    private float lastShootAnimationTime = 0f;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your desired input for shooting (e.g., left mouse button)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2")) // Change "Fire1" to your desired input for shooting (e.g., left mouse button)
        {
            Punch();
        }
        // Assuming 'horizontalInput' and 'verticalInput' are your input values (e.g., from Input.GetAxis)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    }
    private IEnumerator ResetIsAttacking()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed (1.0f for 1 second)
        IsAttacking = false;
        animator.SetBool("IsAttacking", false);
    }
    private IEnumerator ResetIsPunching()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed (1.0f for 1 second)
        IsPunching = false;
        animator.SetBool("IsPunching", false);
    }
    private void Punch()
    {
        if(!IsPunching)
        {
            IsPunching = true; 
            animator.SetBool("IsPunching", true);
            StartCoroutine(ResetIsPunching());
        }
    }
    private void Shoot()
    {
        if (!IsAttacking && Time.time - lastShootAnimationTime >= 1.0f)
            {
                IsAttacking = true;
                animator.SetBool("IsAttacking", true);
                lastShootAnimationTime = Time.time;
                StartCoroutine(ShootAfterDelay());
            }
    }
    private IEnumerator ShootAfterDelay()
        {
            yield return new WaitForSeconds(1.0f); // Adjust the delay as needed (1.0f for 1 second)

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDirection = (mousePos - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position + shootDirection * 2, Quaternion.identity);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * projectileSpeed;

            IsAttacking = false;
            animator.SetBool("IsAttacking", false);
        }
}