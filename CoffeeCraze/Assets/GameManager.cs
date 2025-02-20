using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private DialogueAsset startDialogue;
    [SerializeField] private CoinUI coinUI;  // Fixed the type declaration
    private int currentCurrency;  // Only declare once

    void Start()
    {
        dialoguePanel.ShowDialogue(startDialogue);
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentCurrency()
    {
        return currentCurrency;
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        coinUI.UpdateCoinDisplay(currentCurrency);
    }

    public bool SpendCurrency(int amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            coinUI.UpdateCoinDisplay(currentCurrency);
            return true;
        }
        return false;
    }
}