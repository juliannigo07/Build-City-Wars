using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public static ProjectileFactory Instance { get; private set; }

    [Header("Projectile Prefabs")]
    [SerializeField] private Projectile normalPrefab;
    [SerializeField] private ExplosiveProjectile explosivePrefab;
    [SerializeField] private FreezeProjectile freezePrefab;

    private ObjectPool<Projectile> normalPool;
    private ObjectPool<ExplosiveProjectile> explosivePool;
    private ObjectPool<FreezeProjectile> freezePool;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        normalPool = new ObjectPool<Projectile>(normalPrefab, 20, transform);
        explosivePool = new ObjectPool<ExplosiveProjectile>(explosivePrefab, 10, transform);
        freezePool = new ObjectPool<FreezeProjectile>(freezePrefab, 10, transform);
    }

    public Projectile SpawnNormal(Vector3 pos)
    {
        var proj = normalPool.Get();
        proj.transform.position = pos;
        return proj;
    }

    public ExplosiveProjectile SpawnExplosive(Vector3 pos)
    {
        var proj = explosivePool.Get();
        proj.transform.position = pos;
        return proj;
    }

    public FreezeProjectile SpawnFreeze(Vector3 pos)
    {
        var proj = freezePool.Get();
        proj.transform.position = pos;
        return proj;
    }

    public void Return(Projectile proj) => normalPool.Return(proj);
    public void Return(ExplosiveProjectile proj) => explosivePool.Return(proj);
    public void Return(FreezeProjectile proj) => freezePool.Return(proj);
}
