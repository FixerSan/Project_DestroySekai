using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance 
    {
        get 
        {
            if (instance == null)
            {
                GameObject obj = GameObject.Find($"@{typeof(T).Name}");
                if (obj == null)
                {
                    obj = new GameObject($"@{typeof(T).Name}");
                    obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }

                instance = obj.GetComponent<T>();
            }
            return instance;
        }
    }
    private static T instance;
}
