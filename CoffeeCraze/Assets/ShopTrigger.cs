using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI; // Drag & drop in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && shopUI != null)
        {
            shopUI.OpenShop();
        }
    }
}
