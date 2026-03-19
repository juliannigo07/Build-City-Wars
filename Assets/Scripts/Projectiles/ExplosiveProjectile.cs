using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour, IPoolable
{
    public float speed = 5f;
    public int damage = 4;
    public float explosionRadius = 2f;

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
            Explode();
            ProjectileFactory.Instance.Return(this);
        }
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
