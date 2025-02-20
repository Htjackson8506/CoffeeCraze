using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button attackDamageButton;
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button movementSpeedButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openButton;

    [Header("Cost Texts")]
    [SerializeField] private TextMeshProUGUI attackDamageCostText;
    [SerializeField] private TextMeshProUGUI attackSpeedCostText;
    [SerializeField] private TextMeshProUGUI movementSpeedCostText;

    private void Start()
    {
        if (shopPanel == null)
        {
            Debug.LogError("❌ ShopPanel is not assigned in the Inspector!");
            return;
        }

        shopPanel.SetActive(false);

        attackDamageButton.onClick.AddListener(() => BuyUpgrade("AttackDamage"));
        attackSpeedButton.onClick.AddListener(() => BuyUpgrade("AttackSpeed"));
        movementSpeedButton.onClick.AddListener(() => BuyUpgrade("MovementSpeed"));
        closeButton.onClick.AddListener(CloseShop);
        openButton.onClick.AddListener(OpenShop);

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        int currentCoins = GameManager.Instance.GetCurrentCurrency();

        // Update attack damage button
        if (attackDamageButton != null)
        {
            attackDamageButton.interactable = currentCoins >= 10; // Using a base cost of 10 for now
            if (attackDamageCostText != null)
            {
                attackDamageCostText.text = "Cost: 10";
            }
        }

        // Update attack speed button
        if (attackSpeedButton != null)
        {
            attackSpeedButton.interactable = currentCoins >= 10;
            if (attackSpeedCostText != null)
            {
                attackSpeedCostText.text = "Cost: 10";
            }
        }

        // Update movement speed button
        if (movementSpeedButton != null)
        {
            movementSpeedButton.interactable = currentCoins >= 10;
            if (movementSpeedCostText != null)
            {
                movementSpeedCostText.text = "Cost: 10";
            }
        }
    }

    private void BuyUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "AttackDamage":
                UpgradeSystem.Instance.UpgradeAttackDamage();
                break;
            case "AttackSpeed":
                UpgradeSystem.Instance.UpgradeAttackSpeed();
                break;
            case "MovementSpeed":
                UpgradeSystem.Instance.UpgradeMovementSpeed();
                break;
        }
        UpdateButtonStates();
    }

    public void OpenShop()
    {
        Debug.Log("✅ Shop UI Opened!");
        shopPanel.SetActive(true);
        UpdateButtonStates();
    }

    public void CloseShop()
    {
        Debug.Log("✅ Shop UI Closed!");
        shopPanel.SetActive(false);
    }
}