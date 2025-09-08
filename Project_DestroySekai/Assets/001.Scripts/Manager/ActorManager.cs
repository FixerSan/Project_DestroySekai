using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorManager 
{
    public HashSet<Attacker> attackers = new HashSet<Attacker>();
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

    public Attacker SpawnAttacker(int _index)
    {
        //데이터 매니저에서 뽑았다고 치고
        ActorData data = new ActorData();
        Attacker attacker =  Managers.Resource.Instantiate("Attacker_Test", AttackTrans, true).GetOrAddComponent<Attacker>();
        attacker.Init(data);
        return attacker;
    }
}
