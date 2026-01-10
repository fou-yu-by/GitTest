using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : SingleMonoBase<ObjectPool>
{
    public GameObject objectPrefab;

    public int creatObjectCount;

    public Queue<GameObject> availableObjects = new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        FillPool();
    }


    public void FillPool()
    {
        for(int i = 0; i < creatObjectCount; i++)
        {
            var newObject = Instantiate(objectPrefab);
            newObject.transform.SetParent(transform);

            ReturnPool(newObject);
        }
    }

    public void ReturnPool(GameObject _returnObject)
    {
        _returnObject.SetActive(false);
        availableObjects.Enqueue(_returnObject);
    }

    public void GetGameObjectFromPool()
    {
        if(availableObjects.Count == 0)
        {
            FillPool();
        }
        var outObject = availableObjects.Dequeue();
        outObject.SetActive(true);
    }




}
