using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoffeeShop : MonoBehaviour
{
    [SerializeField] private GameOverManager gameOverManager;
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private RectTransform healthBarFill;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem damageEffect;
    [SerializeField] private AudioClip damageSound;
    
    private int currentHealth;
    private float initialHealthBarWidth;
    private AudioSource audioSource;
    
    public UnityEvent<int> onHealthChanged;
    public UnityEvent onGameOver;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (healthBarFill != null)
        {
            initialHealthBarWidth = healthBarFill.sizeDelta.x;
        }
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
        
        // Play effects
        if (damageEffect != null)
        {
            damageEffect.Play();
        }
        
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBarFill.localScale = new Vector3(healthPercentage, 1, 1);
        }
    }

    private void GameOver()
    {
        onGameOver?.Invoke();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }
}
