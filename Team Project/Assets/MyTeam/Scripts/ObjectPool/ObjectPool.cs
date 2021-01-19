using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool<T> where T : PoolableObject
{
    private int allocateCount;

    public delegate T Initializer();
    private Initializer initializer;

    private Stack<T> objStack;
    public List<T> objList;

    public ObjectPool()
    {
        // default constructor
    }


    public ObjectPool(int ac, Initializer fn)
    {
        this.allocateCount = ac;
        this.initializer = fn;
        this.objStack = new Stack<T>();
        this.objList = new List<T>();
    }

    public void Allocate()
    {
        for (int index = 0; index < this.allocateCount; ++index)
        {
            this.objStack.Push(this.initializer());
        }
    }

    public T PopObject()
    {
        if (this.objStack.Count <= 0)
        {
            Allocate();
        }

        T obj = this.objStack.Pop();
        this.objList.Add(obj);

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void PushObject(T obj)
    {
        obj.gameObject.SetActive(false);

        this.objList.Remove(obj);
        this.objStack.Push(obj);
    }

    public void Dispose()
    {
        if (this.objStack == null || this.objList == null)
            return;

        this.objList.ForEach(obj => this.objStack.Push(obj));

        while (this.objStack.Count > 0)
        {
            GameObject.Destroy(this.objStack.Pop());
        }

        this.objList.Clear();
        this.objStack.Clear();
    }

}