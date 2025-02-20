using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance;

    private int attackSpeedLevel = 0;
    private int projectileSizeLevel = 0;
    private int movementSpeedLevel = 0;

    public float baseAttackSpeed = 1.0f;
    public float baseProjectileSize = 1.0f;
    public float baseMovementSpeed = 5.0f;

    public float attackSpeedMultiplier = 0.1f; // 10% faster per level
    public float projectileSizeMultiplier = 0.2f; // 20% bigger per level
    public float movementSpeedMultiplier = 0.5f; // 0.5 faster per level

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

    public void UpgradeAttackSpeed()
    {
        attackSpeedLevel++;
    }

    public void UpgradeProjectileSize()
    {
        projectileSizeLevel++;
    }

    public void UpgradeMovementSpeed()
    {
        movementSpeedLevel++;
    }

    public float GetAttackSpeed() => baseAttackSpeed - (attackSpeedLevel * attackSpeedMultiplier);
    public float GetProjectileSize() => baseProjectileSize + (projectileSizeLevel * projectileSizeMultiplier);
    public float GetMovementSpeed() => baseMovementSpeed + (movementSpeedLevel * movementSpeedMultiplier);
}
