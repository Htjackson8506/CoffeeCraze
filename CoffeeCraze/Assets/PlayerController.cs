using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Combat")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;
    
    [Header("Effects")]
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip footstepSound;
    
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector2 movement;
    private Vector2 mousePosition;
    private float nextFireTime;
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
    }

    private void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // Mouse position for aiming
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        
        // Shooting
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        
        // Footstep sounds
        if (movement.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        
        // Rotation towards mouse
        //Vector2 lookDirection = mousePosition - rb.position;
        //float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        audioSource.PlayOneShot(shootSound);
    }
}
