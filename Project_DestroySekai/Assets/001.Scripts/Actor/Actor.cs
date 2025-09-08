using UnityEngine;
using UnityEngine.AI;

public abstract class Actor : MonoBehaviour
{
    protected ActorData data;
    
    public abstract void Init(ActorData _data);
}

[System.Serializable]
public class ActorData
{
    public string name;
}
