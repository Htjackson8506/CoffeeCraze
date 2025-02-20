using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount = 5;
        public float spawnInterval = 2f;
        public float timeBetweenWaves = 5f;
        public float enemySpeed = 2f;
        public int enemyHealth = 100;
    }

    [Header("Wave Settings")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    
    [Header("Audio")]
    [SerializeField] private AudioClip waveStartSound;
    
    private AudioSource audioSource;
    private int currentWave = 0;
    private bool isSpawning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeWaves();
        StartCoroutine(StartWaves());
        UpdateWaveUI();
    }

    private void InitializeWaves()
    {
        // Initialize 5 waves with increasing difficulty
        waves = new Wave[5];
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i] = new Wave
            {
                enemyCount = 5 + (i * 2), // Increase enemies per wave
                spawnInterval = 2f - (i * 0.2f), // Spawn faster each wave
                timeBetweenWaves = 5f,
                enemySpeed = 2f + (i * 0.5f), // Enemies get faster
                enemyHealth = 100 + (i * 20) // Enemies get stronger
            };
        }
    }

    private IEnumerator StartWaves()
    {
        while (currentWave < waves.Length)
        {
            if (!isSpawning)
            {
                yield return new WaitForSeconds(waves[currentWave].timeBetweenWaves);
                StartCoroutine(SpawnWave());
                currentWave++;
                UpdateWaveUI();
            }
            yield return null;
        }
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;
        
        if (waveStartSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(waveStartSound);
        }

        Wave currentWaveData = waves[currentWave];

        for (int i = 0; i < currentWaveData.enemyCount; i++)
        {
            SpawnEnemy(currentWaveData);
            yield return new WaitForSeconds(currentWaveData.spawnInterval);
        }

        isSpawning = false;
    }

    private void SpawnEnemy(Wave waveData)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Configure enemy based on wave data
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.SetProperties(waveData.enemySpeed, waveData.enemyHealth);
        }
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave + 1}";
        }
    }
}
