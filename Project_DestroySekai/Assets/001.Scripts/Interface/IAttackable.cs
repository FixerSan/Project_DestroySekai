using UnityEngine;

public interface IAttackable
{
    public int AttackDistance { get; }

    public abstract void Attack(float _force);
}
