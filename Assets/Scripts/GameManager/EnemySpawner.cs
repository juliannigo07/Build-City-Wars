using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject victoryPanel;

    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;

    [Header("Wave Config")]
    [SerializeField] private int totalWaves = 10;
    [SerializeField] private float delayBetweenWaves = 3f;
    [SerializeField] private float spawnInterval = 0.4f;
    [SerializeField] private int healthIncreasePerWave = 2;
    [SerializeField] private float speedIncreasePerWave = 0.2f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(HandleWaves());
    }

   

    IEnumerator HandleWaves()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            UpdateWaveUI();

            int normalCount = 5 + currentWave * 2;
            int fastCount = 2 + currentWave;
            int strongCount = 1 + currentWave / 2;

            yield return StartCoroutine(SpawnEnemies("normal", normalCount));
            yield return StartCoroutine(SpawnEnemies("fast", fastCount));
            yield return StartCoroutine(SpawnEnemies("strong", strongCount));

            yield return new WaitUntil(() => GameManager.Instance.activeEnemies.Count == 0);
            yield return new WaitForSeconds(delayBetweenWaves);
        }

        ScreenManager.Instance.Show("VictoryPanel");
        Time.timeScale = 0f;
    }

    IEnumerator SpawnEnemies(string type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), 0, 0);
            Enemy enemy = EnemyFactory.Instance.SpawnEnemy(type, spawnPoint.position + offset);

            enemy.OnSpawnedFromPool();

            enemy.ResetStats();
            enemy.pathPoints = GameManager.Instance.path;
            enemy.maxHP += healthIncreasePerWave * currentWave;
            enemy.speed += speedIncreasePerWave * currentWave;

            GameManager.Instance.RegisterEnemy(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Oleada: {currentWave} / {totalWaves}";
        }
    }
}