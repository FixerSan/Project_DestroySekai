using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;


public class TowerSpawner : MonoBehaviour
{
    public Define.Tower tower;
    private Transform spawnPoint;

    public void Start()
    {
        spawnPoint = Util.FindChild<Transform>(gameObject, "SpawnPoint");
        Managers.Time.AddTimer(0.3f, () => SpawnTower(Define.Tower.Default));
    }

    public void SpawnTower(Define.Tower _tower)
    {
        Managers.Actor.SpawnTower((int)_tower, spawnPoint.position, spawnPoint);
    }
}
