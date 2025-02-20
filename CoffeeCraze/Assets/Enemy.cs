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
        GameObject coffeeShop = GameObject.FindGameObjectWithTag("CoffeeShop");
        if (coffeeShop != null)
        {
            target = coffeeShop.transform;
        }
    }

    private void Update()
    {
        if (isDead || target == null) return;  // Early return if dead or no target

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

    public void TakeDamage(int amount)
    {
        if (isDead) return;  // Don't process damage if already dead
        
        health -= amount;
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        
        // Play effects before destroying
        if (deathEffect != null)
        {
            ParticleSystem effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration);  // Clean up effect after it's done
        }
        
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        
        // Award currency
        GameManager.Instance.AddCurrency(currencyValue);
        
        // Destroy after a tiny delay to ensure effects start playing
        Destroy(gameObject, 0.1f);
    }

    public void SetProperties(float speed, int health)
    {
        this.speed = speed;
        this.health = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;  // Don't process collision if dead

        if (collision.gameObject.CompareTag("CoffeeShop"))
        {
            CoffeeShop coffeeShop = collision.gameObject.GetComponent<CoffeeShop>();
            if (coffeeShop != null)
            {
                coffeeShop.TakeDamage(damage);
                Die();
            }
        }
    }
}