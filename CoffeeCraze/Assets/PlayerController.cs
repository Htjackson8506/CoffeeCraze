using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed = 5f;
    private float moveSpeed;

    [Header("Combat")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float baseFireRate = 0.5f;
    [SerializeField] private float baseBulletSpeed = 10f;
    [SerializeField] private Transform firePoint;
    private float fireRate;
    private float bulletSpeed;

    [Header("Audio")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float footstepRate = 0.5f; // Time between footsteps

    private Rigidbody2D rb;
    private Camera cam;
    private AudioSource audioSource;
    private Vector2 movement;
    private Vector2 mousePosition;
    private float nextFireTime;
    private float nextFootstepTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = false;

        // Initialize values from the Upgrade System
        ApplyUpgrades();
    }

    private void Update()
    {
        // Get upgraded values every frame
        ApplyUpgrades();

        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Mouse position
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        // Shooting
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // Footsteps
        if (movement.magnitude > 0.1f && Time.time >= nextFootstepTime)
        {
            PlayFootstep();
            nextFootstepTime = Time.time + footstepRate;
        }
    }

    private void FixedUpdate()
    {
        // Movement with upgrade effect
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        Vector2 shootDirection = (mousePosition - rb.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Apply bullet speed upgrade
        bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);

        // Apply bullet size upgrade
        bullet.transform.localScale *= UpgradeSystem.Instance.GetProjectileSize();

        // Play shoot sound
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void PlayFootstep()
    {
        if (footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound, 0.7f); // 0.7f is volume, adjust as needed
        }
    }

    private void ApplyUpgrades()
    {
        if (UpgradeSystem.Instance != null)
        {
            moveSpeed = UpgradeSystem.Instance.GetMovementSpeed();
            fireRate = UpgradeSystem.Instance.GetAttackSpeed();
            bulletSpeed = baseBulletSpeed; // Bullet speed remains the same, but size changes
        }
    }
}
