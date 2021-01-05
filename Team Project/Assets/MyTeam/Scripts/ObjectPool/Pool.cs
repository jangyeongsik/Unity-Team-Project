using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private int allocateCount;
    private Stack<GameObject> objStack;
    public List<GameObject> objList;
    private GameObject pre;


    public Pool(int count,GameObject pref)
    {
        this.allocateCount = count;
        this.pre = pref;
        this.objStack = new Stack<GameObject>();
        this.objList = new List<GameObject>();
    }

    public void Allocate()
    {
        for (int index = 0; index < allocateCount; ++index)
        {
            GameObject pool = Instantiate(pre);
            pool.SetActive(false);
            this.objStack.Push(pool);
        }
    }

    public GameObject PopObject()
    {
        if (this.objStack.Count <= 0)
        {
            Allocate();
        }

        GameObject obj = this.objStack.Pop();
        this.objList.Add(obj);

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void PushObject(GameObject obj)
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
