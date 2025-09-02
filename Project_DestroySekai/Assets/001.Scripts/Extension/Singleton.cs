using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance 
    {
        get 
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find($"@{typeof(T).Name}");
                if (go == null)
                {
                    go = new GameObject($"@{typeof(T).Name}");
                    go.AddComponent<T>();
                }

                instance = go.GetComponent<T>();
            }
            return instance;
        }
    }
    protected static T instance;
}
