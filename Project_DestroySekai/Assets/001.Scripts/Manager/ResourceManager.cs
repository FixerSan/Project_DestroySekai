using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager
{
    private Dictionary<string, Object> resourceDictionary = new Dictionary<string, Object>();

    private string ChangeKey<T>(string _key) where T : Object
    {
        if (typeof(T) == typeof(GameObject)) _key = _key + ".prefab";
        else if (typeof(T) == typeof(TextAsset)) _key = _key + ".json";
        else if (typeof(T) == typeof(AudioClip)) _key = _key + ".AudioClip";
        else if (typeof(T) == typeof(Sprite)) _key = _key + ".sprite";
        else if (typeof(T) == typeof(RuntimeAnimatorController)) _key = _key + ".controller";

        return _key;
    }

    private T CheckLoaded<T>(string _key) where T : Object
    {
        string loadKey = ChangeKey<T>(_key);
        if (resourceDictionary.TryGetValue(loadKey, out Object obj)) return obj as T;
        return null;
    }

    private void LoadAsync<T>(string _key, Action<T> _callback = null) where T : Object
    { 
        string loadkey = ChangeKey<T>(_key);

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadkey);
        asyncOperation.Completed += (load) => 
        {
            if (CheckLoaded<T>(loadkey) == null)
                resourceDictionary.Add(loadkey, load.Result);
            _callback?.Invoke(load.Result);
        };
    }


    public void Load<T>(string _key, Action<T> _callback = null) where T : Object 
    {
        T obj = CheckLoaded<T>(_key);

        if (obj != null)
        {
            _callback?.Invoke(obj as T);
            return;
        }

        LoadAsync<T>(_key, _callback);
    }

    public T Load<T>(string _key) where T : Object
    {
        return CheckLoaded<T>(_key);
    }

    public void LoadAsyncAll<T>(string _label, Action<string,int,int> _progressCallback, Action _completedCallback) where T : Object
    {
        var asyncAllOperation = Addressables.LoadResourceLocationsAsync(_label, typeof(T));

        asyncAllOperation.Completed += (load) => 
        {
            int currentLoadCount = 0;
            int totalLoadCount = asyncAllOperation.Result.Count;

            for (int i = 0; i < totalLoadCount; i++)
            {
                LoadAsync<T>(asyncAllOperation.Result[i].PrimaryKey, (obj) =>
                {
                    currentLoadCount++;
                    _progressCallback?.Invoke(asyncAllOperation.Result[i].PrimaryKey, currentLoadCount, totalLoadCount);
                    if (currentLoadCount == totalLoadCount) _completedCallback?.Invoke();
                });
            }

        };
    }

    public GameObject Instantiate(string _key, Transform _parent = null, bool _pooling = false)
    {
        GameObject obj = CheckLoaded<GameObject>(_key);
        if(obj == null)
        {
            Debug.LogError("프리팹이 로드되어있지 않습니다.");
            return null;
        }

        if(_pooling)
        {
            GameObject poolingObj = Managers.Pool.Get(obj);
            poolingObj.transform.SetParent(_parent);
            return poolingObj;
        }

        GameObject instantiateObj = Object.Instantiate(obj, _parent);
        instantiateObj.name = obj.name;
        return instantiateObj;
    }

    public void Destroy(GameObject _obj)
    {
        if (_obj == null) return;
        if (Managers.Pool.Push(_obj)) return;

        Object.Destroy(_obj);
    }

}
