using UnityEngine;

public class Managers : Singleton<Managers>
{
    //�Ŵ��� ������Ƽ
    public static PoolManager Pool { get { return Instance.pool; } }
    public static GameManager Game { get { return GameManager.Instance; } }

    //�Ŵ��� ����
    private PoolManager pool = new PoolManager();
}
