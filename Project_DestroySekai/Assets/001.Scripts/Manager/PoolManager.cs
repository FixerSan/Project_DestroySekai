using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Ǯ ��ųʸ�
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    //��� Ǯ �ʱ�ȭ
    public void Clear()
    {
        foreach (Pool pool in poolDictionary.Values)
            pool.Clear();
        poolDictionary.Clear();
    }

    //�ش� Ű�� Ǯ�� �����ϴ��� üũ
    public bool CheckExistPool(GameObject _poolObject) => poolDictionary.ContainsKey(_poolObject.name);
    public bool CheckExistPool(string _key) => poolDictionary.ContainsKey(_key);


    //Ǯ ����
    public void CreatePool(GameObject _poolObject, Action _callback = null)
    {
        if (CheckExistPool(_poolObject)) return;

        Pool pool = new Pool(_poolObject);
        poolDictionary.Add(_poolObject.name, pool);
        _callback?.Invoke();
    }

    //Ǯ ����
    public void DeletePool(string _key)
    {
        if (!CheckExistPool(_key)) return;

        poolDictionary[_key].Clear(); 
        poolDictionary.Remove(_key);
    }

    //Ǯ���� ������Ʈ ��������
    public GameObject Get(GameObject _poolObject)
    {
        if (!CheckExistPool(_poolObject))
            CreatePool(_poolObject);

        return poolDictionary[_poolObject.name].Get();
    }

    //Ǯ�� ������Ʈ ����ֱ�
    //TODO :: ���߿� bool���� ������ �ʿ䰡 ���� �� �ڵ� Ǯ ���� �ڵ�� ����
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
    private GameObject prefab;  //���� ������
    private Queue<GameObject> poolObjectQueue;  //���� �� ������Ʈ ť
    private Transform poolTrans;    //ť ��ġ 
    private string poolName;    //Ǯ �̸�


    //Ǯ �����۾�
    public Pool(GameObject _prefab)
    {
        prefab = _prefab;
        poolName = $"{_prefab.name}Pool";
        poolObjectQueue = new Queue<GameObject>();
        Init();
    }

    //�ʱ�ȭ
    public void Init()
    {
        GameObject poolParent = GameObject.Find("@Pool");
        if (poolParent != null) poolParent = new GameObject("@Pool");

        poolTrans = new GameObject(name: poolName).transform;
        poolTrans.SetParent(poolParent.transform);
    }

    //Ǯ���� ������Ʈ ��������
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

    //������Ʈ�� Ǯ�� ����ֱ�
    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(poolTrans);
        _poolObject.SetActive(false);
        poolObjectQueue.Enqueue(_poolObject);
    }

    //Ǯ ���� �� �ʱ�ȭ
    public void Clear()
    {
        poolObjectQueue?.Clear();
        prefab = null;
        GameObject.Destroy(poolTrans.gameObject);
        poolName = string.Empty;
    }
}
