using JetBrains.Annotations;
using UnityEngine;

public class TowerController : Actor
{
    public override void Init(ActorData _data, ActorStatus _status)
    {
        data = _data;
        status = _status;

        status.maxHP = 10f;
        status.nowHP = status.maxHP;
        status.speed = data.speed;
        status.attackForce = 3f;
    }
}
