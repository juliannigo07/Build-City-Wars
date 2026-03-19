using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    public float speed = 2f;
    public int maxHP = 10;
    private int currentHP;
    public int reward = 6;

    public Transform[] pathPoints;
    private int currentTargetIndex = 0;

    [HideInInspector] public int baseHP;
    [HideInInspector] public float baseSpeed;

    void Awake()
    {
        baseHP = maxHP;
        baseSpeed = speed;
    }

    public void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Length == 0 || currentTargetIndex >= pathPoints.Length)
            return;

        Transform target = pathPoints[currentTargetIndex];
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= step)
        {
            transform.position = target.position;
            currentTargetIndex++;

            if (currentTargetIndex >= pathPoints.Length)
            {
                GameManager.Instance.EnemyReachedEnd(this);
                Die(); 
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void ResetStats()
    {
        maxHP = baseHP;
        speed = baseSpeed;
        currentHP = maxHP;
    }

    public void OnSpawnedFromPool()
    {
        currentTargetIndex = 0;
        if (pathPoints != null && pathPoints.Length > 0)
        {
            transform.position = pathPoints[0].position;
        }
        currentHP = maxHP;
        gameObject.SetActive(true);
    }

    public void OnReturnedToPool()
    {
        
    }

    void Die()
    {
        EnemyFactory.Instance.ReturnEnemy(this);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            GameManager.Instance.OnEnemyKilled(this);
            GameEconomy.Instance.GanarDinero(reward);
            Die();
        }
    }

    public int GetCurrentHP() => currentHP;
}
