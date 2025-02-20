using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance;

    [Header("Upgrade Levels")]
    private int attackDamageLevel = 0;
    private int attackSpeedLevel = 0;
    private int movementSpeedLevel = 0;
    private const int MAX_LEVEL = 5;

    [Header("Base Stats")]
    public int baseAttackDamage = 20;
    public float baseAttackSpeed = 0.05f;
    public float baseMovementSpeed = 5.0f;

    [Header("Upgrade Multipliers")]
    public float attackSpeedMultiplier = 0.05f;
    public float movementSpeedMultiplier = 0.5f;

    [Header("Upgrade Costs")]
    public int baseCost = 10;
    public int costIncreasePerLevel = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int GetUpgradeCost(int currentLevel)
    {
        return baseCost + (costIncreasePerLevel * currentLevel);
    }

    public void UpgradeAttackDamage()
    {
        if (attackDamageLevel >= MAX_LEVEL)
        {
            Debug.Log("Attack Damage at max level!");
            return;
        }

        int cost = GetUpgradeCost(attackDamageLevel);
        if (GameManager.Instance.SpendCurrency(cost))
        {
            attackDamageLevel++;
            Debug.Log($"Attack Damage upgraded to level {attackDamageLevel}");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (attackSpeedLevel >= MAX_LEVEL)
        {
            Debug.Log("Attack Speed at max level!");
            return;
        }

        int cost = GetUpgradeCost(attackSpeedLevel);
        if (GameManager.Instance.SpendCurrency(cost))
        {
            attackSpeedLevel++;
            Debug.Log($"Attack Speed upgraded to level {attackSpeedLevel}");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void UpgradeMovementSpeed()
    {
        if (movementSpeedLevel >= MAX_LEVEL)
        {
            Debug.Log("Movement Speed at max level!");
            return;
        }

        int cost = GetUpgradeCost(movementSpeedLevel);
        if (GameManager.Instance.SpendCurrency(cost))
        {
            movementSpeedLevel++;
            Debug.Log($"Movement Speed upgraded to level {movementSpeedLevel}");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public bool IsMaxLevel(string upgradeType)
    {
        switch (upgradeType)
        {
            case "AttackDamage":
                return attackDamageLevel >= MAX_LEVEL;
            case "AttackSpeed":
                return attackSpeedLevel >= MAX_LEVEL;
            case "MovementSpeed":
                return movementSpeedLevel >= MAX_LEVEL;
            default:
                return true;
        }
    }

    public int GetUpgradeCost(string upgradeType)
    {
        int currentLevel = 0;
        switch (upgradeType)
        {
            case "AttackDamage":
                currentLevel = attackDamageLevel;
                break;
            case "AttackSpeed":
                currentLevel = attackSpeedLevel;
                break;
            case "MovementSpeed":
                currentLevel = movementSpeedLevel;
                break;
        }
        return baseCost + (costIncreasePerLevel * currentLevel);
    }

    public int GetAttackDamage() => baseAttackDamage + attackDamageLevel * 10;
    public float GetAttackSpeed() => baseAttackSpeed - (attackSpeedLevel * attackSpeedMultiplier);
    public float GetMovementSpeed() => baseMovementSpeed + (movementSpeedLevel * movementSpeedMultiplier);
}