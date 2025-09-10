using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager 
{
    public List<AttackerController> attackers = new List<AttackerController>();
    public List<TowerController> towers = new List<TowerController>();
    public Transform AttackTrans
    {
        get
        {
            if(attackTrans == null)
            {
                GameObject obj = GameObject.Find($"AttackTrans");
                if (obj == null)
                    obj = new GameObject($"@AttackTrans");
                attackTrans = obj.transform;
            }

            return attackTrans;
        }
    }

    private Transform attackTrans;

    public ActorManager()
    {

    }

    public AttackerController SpawnAttacker(int _index, Vector3 _position, Transform _parent = null)
    {
        //������ �Ŵ������� �̾Ҵٰ� ġ��
        ActorData data = new ActorData();
        //����� �����Ϳ� �� ���� �־����
        data.name = "Attacker_Test";


        AttackerController attacker =  Managers.Resource.Instantiate(data.name, _position, AttackTrans, true).GetOrAddComponent<AttackerController>();

        ActorStatus status = new ActorStatus();

        attacker.Init(data, status);
        attackers.Add(attacker);
        return attacker;
    }

    public TowerController SpawnTower(int _index, Vector3 _position, Transform _parent = null)
    {
        //������ �Ŵ������� �̾Ҵ� ġ��
        ActorData data = new ActorData();
        //����� �����Ϳ� �� ���� �־����
        data.name = "Tower_Default";
        ActorStatus status = new ActorStatus();


        TowerController tower = Managers.Resource.Instantiate(data.name, _position, _parent).GetOrAddComponent<TowerController>();
        tower.Init(data, status);   
        towers.Add(tower);
        return tower;
    }
}
