using UnityEngine;
using UnityEngine.Events;

public class CoffeeShop : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    public UnityEvent<int> onHealthChanged;
    public UnityEvent onGameOver;

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        onHealthChanged?.Invoke(currentHealth);
        
        if (currentHealth <= 0)
        {
            onGameOver?.Invoke();
        }
    }
}
