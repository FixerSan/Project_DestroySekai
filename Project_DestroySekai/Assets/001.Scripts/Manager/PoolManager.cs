using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    public void Clear()
    {
        foreach (Pool pool in poolDictionary.Values)
            pool.Clear();
        poolDictionary.Clear();
    }
    public bool CheckExistPool(GameObject _poolObject) => poolDictionary.ContainsKey(_poolObject.name);
    public bool CheckExistPool(string _key) => poolDictionary.ContainsKey(_key);

    public void CreatePool(GameObject _poolObject, Action _callback = null)
    {
        if (CheckExistPool(_poolObject)) return;

        Pool pool = new Pool(_poolObject);
        poolDictionary.Add(_poolObject.name, pool);
        _callback?.Invoke();
    }

    public void DeletePool(string _key)
    {
        if (!CheckExistPool(_key)) return;

        poolDictionary[_key].Clear(); 
        poolDictionary.Remove(_key);
    }

    public GameObject Get(GameObject _poolObject)
    {
        if (!CheckExistPool(_poolObject))
            CreatePool(_poolObject);

        return poolDictionary[_poolObject.name].Get();
    }

    //TODO :: 나중에 bool값을 리턴할 필요가 없을 시 자동 풀 생성 코드로 변경
    public bool Push(GameObject _poolObject)
    {
        if (CheckExistPool(_poolObject))
        {
            poolDictionary[_poolObject.name].Push(_poolObject);
            return true;
        }
        return false;
    }
}

public class Pool
{
    private GameObject prefab;  //원본 프리팹
    private Queue<GameObject> poolObjectQueue;  //재사용 될 오브젝트 큐
    private Transform poolTrans;    //큐 위치 
    private string poolName;    //풀 이름

    public Pool(GameObject _prefab)
    {
        prefab = _prefab;
        poolName = $"{_prefab.name}Pool";
        poolObjectQueue = new Queue<GameObject>();
        Init();
    }

    public void Init()
    {
        GameObject poolParent = GameObject.Find("@Pool");
        if (poolParent != null) poolParent = new GameObject("@Pool");

        poolTrans = new GameObject(name: poolName).transform;
        poolTrans.SetParent(poolParent.transform);
    }

    public GameObject Get()
    {
        GameObject poolObject;
        if(!poolObjectQueue.TryDequeue(out poolObject))
        {
            poolObject = GameObject.Instantiate(prefab);
            poolObject.name = prefab.name;
        }

        poolObject.SetActive(true);
        return poolObject;
    }

    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(poolTrans);
        _poolObject.SetActive(false);
        poolObjectQueue.Enqueue(_poolObject);
    }

    public void Clear()
    {
        poolObjectQueue?.Clear();
        prefab = null;
        GameObject.Destroy(poolTrans.gameObject);
        poolName = string.Empty;
    }
}
