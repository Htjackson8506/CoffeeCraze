using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int startingCurrency = 100;
    private int currentCurrency;

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

    void Start()
    {
        currentCurrency = startingCurrency;
    }

    public bool SpendCurrency(int amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            return true;
        }
        return false;
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
    }
}
