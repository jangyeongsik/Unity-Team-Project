using UnityEngine;
using System.Collections;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager singleton;
    public static ObjectPoolManager GetInstance() { return singleton; }

    public GameObject PreFap;                       //해골 궁수용
    public GameObject PreFap2;                      //사제용

    public Pool objectPool;
    public Pool objectPool2;
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
        objectPool = new Pool(10, PreFap);          //해골 궁수용
        objectPool2 = new Pool(10, PreFap2);        //사제용

        objectPool.Allocate();
        objectPool2.Allocate();
    }


    void Destroy()
    {
        objectPool.Dispose();
        objectPool2.Dispose();
        singleton = null;
    }

}
