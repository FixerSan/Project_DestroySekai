using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<Transform> pathList = new List<Transform>();

    public void Awake()
    {
        SetPathPoses();
    }

    public void SetPathPoses()
    {
        Transform pathes = GameObject.Find("Path").transform;

        for (int i = 0; i < pathes.childCount; i++)
        {
            pathList.Add(pathes.GetChild(i).transform);
        }
    }

    public int GetEndPathIndex()
    {
        return pathList.Count - 1;
    }

    public Transform GetPathPos(int _pathIndex)
    {
        return pathList[_pathIndex];
    }
}
