using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance;

    private int attackDamageLevel = 0;
    private int attackSpeedLevel = 0;
    private int movementSpeedLevel = 0;

    public int baseAttackDamage = 20;
    public float baseAttackSpeed = 0.5f;
    public float baseMovementSpeed = 5.0f;

    
    public float attackSpeedMultiplier = 0.1f; // Reduces cooldown by 10% per level
    public float movementSpeedMultiplier = 0.5f; // Increases speed by 0.5 per level

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

    public void UpgradeAttackDamage()
    {
        attackDamageLevel++;
    }

    public void UpgradeAttackSpeed()
    {
        attackSpeedLevel++;
    }

    public void UpgradeMovementSpeed()
    {
        movementSpeedLevel++;
    }

    public int GetAttackDamage() => baseAttackDamage + attackDamageLevel * 10;
    public float GetAttackSpeed() => baseAttackSpeed - (attackSpeedLevel * attackSpeedMultiplier);
    public float GetMovementSpeed() => baseMovementSpeed + (movementSpeedLevel * movementSpeedMultiplier);
}
