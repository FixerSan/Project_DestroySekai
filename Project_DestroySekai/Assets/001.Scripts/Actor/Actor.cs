using UnityEngine;
using UnityEngine.AI;

public abstract class Actor : MonoBehaviour
{
    protected ActorData data;
    public ActorStatus status;
    
    public abstract void Init(ActorData _data, ActorStatus _status);
}

[System.Serializable]
public class ActorData
{
    public int index;
    public string name;
    public float hp;
    public float speed;
    public float attackForce;
}

public class ActorStatus
{
    public float maxHP;
    public float nowHP;

    public float speed;
    public float attackForce;
}
