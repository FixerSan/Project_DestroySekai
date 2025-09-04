using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //풀 딕셔너리
    public Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    //모든 풀 초기화
    public void Clear()
    {
        foreach (Pool pool in poolDictionary.Values)
            pool.Clear();
        poolDictionary.Clear();
    }

    //해당 키의 풀이 존재하는지 체크
    public bool CheckExistPool(GameObject _poolObject) => poolDictionary.ContainsKey(_poolObject.name);
    public bool CheckExistPool(string _key) => poolDictionary.ContainsKey(_key);


    //풀 생성
    public void CreatePool(GameObject _poolObject, Action _callback = null)
    {
        if (CheckExistPool(_poolObject)) return;

        Pool pool = new Pool(_poolObject);
        poolDictionary.Add(_poolObject.name, pool);
        _callback?.Invoke();
    }

    //풀 삭제
    public void DeletePool(string _key)
    {
        if (!CheckExistPool(_key)) return;

        poolDictionary[_key].Clear(); 
        poolDictionary.Remove(_key);
    }

    //풀에서 오브젝트 가져오기
    public GameObject Get(GameObject _poolObject)
    {
        if (!CheckExistPool(_poolObject))
            CreatePool(_poolObject);

        return poolDictionary[_poolObject.name].Get();
    }

    //풀에 오브젝트 집어넣기
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


    //풀 생성작업
    public Pool(GameObject _prefab)
    {
        prefab = _prefab;
        poolName = $"{_prefab.name}Pool";
        poolObjectQueue = new Queue<GameObject>();
        Init();
    }

    //초기화
    public void Init()
    {
        GameObject poolParent = GameObject.Find("@Pool");
        if (poolParent != null) poolParent = new GameObject("@Pool");

        poolTrans = new GameObject(name: poolName).transform;
        poolTrans.SetParent(poolParent.transform);
    }

    //풀에서 오브젝트 내보내기
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

    //오브젝트를 풀에 집어넣기
    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(poolTrans);
        _poolObject.SetActive(false);
        poolObjectQueue.Enqueue(_poolObject);
    }

    //풀 삭제 및 초기화
    public void Clear()
    {
        poolObjectQueue?.Clear();
        prefab = null;
        GameObject.Destroy(poolTrans.gameObject);
        poolName = string.Empty;
    }
}
