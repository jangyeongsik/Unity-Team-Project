using UnityEngine;
using System.Collections;

public class ObjectPoolManager : SingletonMonobehaviour<ObjectPoolManager>
{
    private static ObjectPoolManager singleton;
    public static ObjectPoolManager GetInstance() { return singleton; }

    public ObjectPool<PoolableObject> bulletPool = new ObjectPool<PoolableObject>();

    //public Bullet bulletPrefab;

    void Start()
    {
        /*bulletPool = new ObjectPool<PoolableObject>(5, () =>
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.Create(bulletPool);
            return bullet;
        });*/

       // bulletPool.Allocate();
    }

    /*void OnDestroy()
    {
        bulletPool.Dispose();
        singleton = null;
    }*/

}
