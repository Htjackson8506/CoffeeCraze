using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private GameObject hitEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}