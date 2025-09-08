using UnityEngine;

public class Managers : Singleton<Managers>
{
    //�Ŵ��� ������Ƽ
    public static PoolManager Pool { get { return Instance.pool; } }
    public static ResourceManager Resource { get { return Instance.resource; } }
    public static GameManager Game { get { return GameManager.Instance; } }
    public static ActorManager Actor { get { return Instance.actor; } }

    //�Ŵ��� ����
    private PoolManager pool = new PoolManager();
    private ResourceManager resource = new ResourceManager();
    private ActorManager actor = new ActorManager();
}
