using UnityEngine;

public class Managers : Singleton<Managers>
{
    //매니저 프로퍼티
    public static PoolManager Pool { get { return Instance.pool; } }
    public static GameManager Game { get { return GameManager.Instance; } }

    //매니저 변수
    private PoolManager pool = new PoolManager();
}
