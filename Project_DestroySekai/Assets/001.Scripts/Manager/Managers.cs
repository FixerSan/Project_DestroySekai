using UnityEngine;

public class Managers : Singleton<Managers>
{
    //�Ŵ��� ������Ƽ
    public GameManager Game { get { return GameManager.Instance; } }

    //�Ŵ��� ����
}
