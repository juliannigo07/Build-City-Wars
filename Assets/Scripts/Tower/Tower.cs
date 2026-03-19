using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    public enum BulletType { Normal, Explosive, Slow }

    [Header("General Settings")]
    public BulletType bulletType = BulletType.Normal;
    public float range = 3f;
    public float fireRate = 1f;
    public int damage = 1;

    [Header("Prefabs")]
    public GameObject normalProjectilePrefab;
    public GameObject explosiveProjectilePrefab;
    public GameObject slowProjectilePrefab;
    public Transform shootPoint;

    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        var enemies = GameManager.Instance.activeEnemies;
        var target = enemies.FirstOrDefault(e => Vector3.Distance(e.transform.position, transform.position) < range);

        if (target != null && fireCooldown <= 0f)
        {
            FireAt(target);
            fireCooldown = 1f / fireRate;
        }
    }

    void FireAt(Enemy enemy)
    {
        if (shootPoint == null) return;

        switch (bulletType)
        {
            case BulletType.Normal:
                var normal = ProjectileFactory.Instance.SpawnNormal(shootPoint.position);
                normal.SetTarget(enemy);
                normal.damage = damage;
                break;

            case BulletType.Explosive:
                var expl = ProjectileFactory.Instance.SpawnExplosive(shootPoint.position);
                expl.SetTarget(enemy);
                expl.damage = damage;
                break;

            case BulletType.Slow:
                var slow = ProjectileFactory.Instance.SpawnFreeze(shootPoint.position);
                slow.SetTarget(enemy);
                slow.damage = damage;
                break;
        }
    }

   

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        if (shootPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPoint.position, shootPoint.position + shootPoint.up * 2f);
        }
    }
}