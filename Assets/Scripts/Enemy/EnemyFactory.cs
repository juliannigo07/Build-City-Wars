using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [Header("Enemy Prefabs")]
    [SerializeField] private Enemy normalPrefab;
    [SerializeField] private Enemy fastPrefab;
    [SerializeField] private Enemy strongPrefab;

    private Dictionary<string, ObjectPool<Enemy>> enemyPools = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        
        enemyPools["normal"] = new ObjectPool<Enemy>(normalPrefab, 10, transform);
        enemyPools["fast"] = new ObjectPool<Enemy>(fastPrefab, 10, transform);
        enemyPools["strong"] = new ObjectPool<Enemy>(strongPrefab, 10, transform);
    }

    public Enemy SpawnEnemy(string type, Vector3 position)
    {
        var enemy = enemyPools[type].Get();
        enemy.transform.position = position;
        enemy.OnSpawnedFromPool();
        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        string type = enemy.tag.ToLower();
        if (enemyPools.ContainsKey(type))
        {
            enemyPools[type].Return(enemy);
        }
        else
        {
            Destroy(enemy.gameObject);
        }
    }
}
