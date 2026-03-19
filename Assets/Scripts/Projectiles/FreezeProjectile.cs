using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : MonoBehaviour, IPoolable
{
    public float speed = 5f;
    public int damage = 1;
    public float slowMultiplier = 0.5f;
    public float slowDuration = 3f;

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
            StartCoroutine(ApplySlow(target));
            ProjectileFactory.Instance.Return(this);
        }
    }

    IEnumerator ApplySlow(Enemy enemy)
    {
        float originalSpeed = enemy.speed;
        enemy.speed *= slowMultiplier;
        yield return new WaitForSeconds(slowDuration);
        enemy.speed = originalSpeed;
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
