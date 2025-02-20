using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountText;  // Reference to your text box that's next to the coin image

    private void Start()
    {
        // Initialize with 0
        UpdateCoinDisplay(0);
    }

    public void UpdateCoinDisplay(int amount)
    {
        if (coinCountText != null)
        {
            coinCountText.text = amount.ToString();  // Just show the number since we have the coin image
        }
        else
        {
            Debug.LogWarning("Coin count text is not assigned in CoinUI!");
        }
    }
}