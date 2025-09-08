using UnityEngine;
using System;
using Unity.VisualScripting;

public class SceneManager
{
    private Transform sceneTrans;
    public Transform SceneTrans
    { 
        get
        {
            if(sceneTrans == null)
            {
                GameObject obj = GameObject.Find($"@{sceneTrans}");
                if (obj == null)
                    obj = new GameObject($"@{sceneTrans}");
                sceneTrans = obj.transform;
                UnityEngine.Object.DontDestroyOnLoad(obj);
            }
            return sceneTrans;
        }
    }

    public Define.Scene currentScene;
    private bool isLoading = false;
    private Action loadCallback;


    public void RemoveScene(Define.Scene _scene, Action _callback = null)
    {
        SceneBase sb = null;
        switch(_scene)
        {
            case Define.Scene.SampleScene:
                sb = SceneTrans.GetComponent<SampleScene>();
                break;
        }

        if(sb != null)
        {
            sb.Clear();
            UnityEngine.Object.Destroy(sb);
        }

        _callback?.Invoke();
    }

    public void AddScene(Define.Scene _scene, Action _callback = null)
    {
        SceneBase sb = null;

        switch (_scene)
        {
            case Define.Scene.SampleScene:
                sb = SceneTrans.AddComponent<SampleScene>();
                break;
        }
        _callback?.Invoke();
    }


    public void LoadScene(Define.Scene _scene, Action _loadCallback = null)
    {
        if (isLoading) return;
        isLoading = true;
        loadCallback = _loadCallback;
        Managers.Pool.Clear();
        //UIÃÊ±âÈ­
        //Manager.UI.CloseAllPopupUI();

        RemoveScene(currentScene, () => 
        {
            currentScene = _scene;
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"{_scene}");
            async.completed += (complete) => 
            {
                AddScene(currentScene, () => { loadCallback?.Invoke(); });
                isLoading = false;
            };
        });
    }

    public T GetScene<T>() where T : SceneBase
    {
        return SceneTrans.GetComponent<T>();
    }
}
