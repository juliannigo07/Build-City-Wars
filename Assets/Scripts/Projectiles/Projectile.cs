using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    public float speed = 5f;
    public int damage = 1;

    private Enemy target;

    public void SetTarget(Enemy enemy)
    {
        target = enemy;
    }

    void Update()
    {
        if (target == null)
        {
            ProjectileFactory.Instance.Return(this);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.TakeDamage(damage);
            ProjectileFactory.Instance.Return(this);
        }
    }

    public void OnSpawnedFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnedToPool()
    {
        gameObject.SetActive(false);
    }
}
