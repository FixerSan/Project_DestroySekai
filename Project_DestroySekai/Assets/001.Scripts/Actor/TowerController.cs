using JetBrains.Annotations;
using UnityEngine;

public class TowerController : Actor, IAttackable
{
    public int AttackDistance { get => 3; }
    public AttackerController attacker;

    private bool isCanAttack = true;

    public override void Init(ActorData _data, ActorStatus _status)
    {
        data = _data;
        status = _status;

        status.maxHP = 10f;
        status.nowHP = status.maxHP;
        status.speed = data.speed;
        status.attackSpeed = data.attackSpeed;
        status.attackSpeed = 1;
        status.attackForce = 3f;
    }

    public void Update()
    {
        if (isCanAttack) Attack(status.attackForce);
    }

    public void Attack(float _force)
    {
        FindAttacker();
        if (attacker == null) return;

        attacker.Hit(_force);
        isCanAttack = false;
        Managers.Time.AddTimer(status.attackSpeed, ()=>isCanAttack =true);
    }

    public void FindAttacker()
    {
        if (Managers.Actor.attackers.Count == 0) return;
        if (attacker != null && CheckAttackDistance(attacker)) return;


        for (int i = 0; i < Managers.Actor.attackers.Count; i++)
        {
            if (!CheckAttackDistance(Managers.Actor.attackers[i]))
                continue;

            attacker = Managers.Actor.attackers[i];
            break;
        }
    }

    public bool CheckAttackDistance(AttackerController _attacker)
    {
        return AttackDistance > Vector3.Distance(_attacker.transform.position, transform.position);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
