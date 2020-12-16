using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolableObject : MonoBehaviour
{
    protected ObjectPool<PoolableObject> pPool;

    public virtual void Create(ObjectPool<PoolableObject> pool)
    {
        pPool = pool;

        gameObject.SetActive(false);
    }

    public virtual void Dispose()
    {
        pPool.PushObject(this);
    }

    public virtual void _OnEnableContents()
    {
        // to do ...
    }

    public virtual void _OnDisableContents()
    {
        // to do ...
    }

}