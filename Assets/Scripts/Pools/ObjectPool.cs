using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;
    private List<GameObject> pool = new List<GameObject>();

    public bool isAutoExpanded;

    public ObjectPool(GameObject prefab, int count, bool isAutoExpanded)
    {
        this.prefab = prefab;
        this.isAutoExpanded = isAutoExpanded;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        var obj = Object.Instantiate(prefab);
        obj.gameObject.SetActive(isActiveByDefault);
        pool.Add(obj);
        return obj;
    }

    private bool HasFreeElement(out GameObject element)
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                return true;
            }
        }
        element = null;
        return false;
    }

    public GameObject GetElement()
    {
        GameObject result = null;
        if (HasFreeElement(out var element)) result = element;
        else if (!isAutoExpanded) throw new System.Exception("There is no free elements");
        else result = CreateObject();
        result.gameObject.SetActive(true);
        return result;
    }

    public List<GameObject> GetFreeElements()
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy) result.Add(pool[i]);
        }
        return result;
    }

    public List<GameObject> GetBusyElements()
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject.activeInHierarchy) result.Add(pool[i]);
        }
        return result;
    }
}