using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button attackDamageButton;
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button movementSpeedButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button openButton;

    private void Start()
    {
        if (shopPanel == null)
        {
            Debug.LogError("❌ ShopPanel is not assigned in the Inspector!");
            return;
        }

        shopPanel.SetActive(false); // Start with shop closed

        attackDamageButton.onClick.AddListener(() => BuyUpgrade("AttackDamage"));
        attackSpeedButton.onClick.AddListener(() => BuyUpgrade("AttackSpeed"));
        movementSpeedButton.onClick.AddListener(() => BuyUpgrade("MovementSpeed"));
        closeButton.onClick.AddListener(CloseShop);
        openButton.onClick.AddListener(OpenShop);
        
    }

    public void OpenShop()
    {
        Debug.Log("✅ Shop UI Opened!");
        shopPanel.SetActive(true);
        //Time.timeScale = 0; // Pause game
    }

    public void CloseShop()
    {
        Debug.Log("✅ Shop UI Closed!");
        shopPanel.SetActive(false);
        //Time.timeScale = 1; // Resume game
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
    }
}
