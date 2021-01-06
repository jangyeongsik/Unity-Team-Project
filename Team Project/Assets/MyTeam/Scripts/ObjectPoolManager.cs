using UnityEngine;
using System.Collections;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager singleton;
    public static ObjectPoolManager GetInstance() { return singleton; }

    public GameObject PreFap;

    public Pool objectPool;
    void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
    }

    private void Start()
    {
        
        objectPool = new Pool(5, PreFap);

        objectPool.Allocate();

    }


    void Destroy()
    {
        objectPool.Dispose();
        singleton = null;
    }

}
