using UnityEngine;
using System.Collections;


public class TowerSpawner : MonoBehaviour
{
    public Define.Tower tower;
    private Transform spawnPoint;

    public void Awake()
    {
        spawnPoint = Util.FindChild<Transform>(gameObject, "SpawnPoint");
        StartCoroutine(Test());
    }

   public IEnumerator Test()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnTower(Define.Tower.Default);
    }

    public void SpawnTower(Define.Tower _tower)
    {
        Managers.Actor.SpawnTower((int)_tower, spawnPoint.position, spawnPoint);
    }
}
