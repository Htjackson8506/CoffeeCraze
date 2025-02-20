using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;
    [SerializeField] private float attractSpeed = 5f;
    [SerializeField] private float attractRadius = 2f;
    
    private Transform player;
    private bool isAttracting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attractRadius)
            {
                isAttracting = true;
            }

            if (isAttracting)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, 
                    attractSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddCurrency(coinValue);
            Destroy(gameObject);
        }
    }
}