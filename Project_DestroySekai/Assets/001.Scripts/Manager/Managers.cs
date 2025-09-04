using UnityEngine;

public class Managers : Singleton<Managers>
{
    //매니저 프로퍼티
    public GameManager Game { get { return GameManager.Instance; } }

    //매니저 변수
}
