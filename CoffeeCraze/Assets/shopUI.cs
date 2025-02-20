using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject shopPanel;
    public Button attackDamageButton;
    public Button attackSpeedButton;
    public Button movementSpeedButton;
    public Button closeButton;

    private void Start()
    {
        shopPanel.SetActive(false);

        attackDamageButton.onClick.AddListener(() => BuyUpgrade("AttackDamage"));
        attackSpeedButton.onClick.AddListener(() => BuyUpgrade("AttackSpeed"));
        movementSpeedButton.onClick.AddListener(() => BuyUpgrade("MovementSpeed"));
        closeButton.onClick.AddListener(CloseShop);
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
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
        CloseShop();
    }
}
