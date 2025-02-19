using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private int currencyValue = 10;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private AudioClip deathSound;
    
    private Transform target;
    private bool isDead;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("CoffeeShop").transform;
    }

    private void Update()
    {
        if (!isDead && target != null)
        {
            // Move towards coffee shop
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
            
            // Rotate towards movement direction
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        
        // Spawn effects
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        // Play sound
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        
        // Award currency
        GameManager.Instance.AddCurrency(currencyValue);
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CoffeeShop"))
        {
            collision.gameObject.GetComponent<CoffeeShop>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}