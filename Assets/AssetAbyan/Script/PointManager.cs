using System;
using System.Collections.Generic;
using UnityEngine;

public enum PointsNames
{
    LeftVagrant, RightVagrant, Villager, LeftArcher, RightArcher, Knight
}
public enum EnemyPointNames{
    Left,Right
}
[Serializable]
public class Points
{
    public PointsNames pointsNames;
    public Transform pointA, pointB;
    public int NPCCount;
}
[Serializable]
public class EnemyPoints{
    public EnemyPointNames enemyPointNames;
    public Transform pointA, pointB;
    public int enemyCount;
}


public class PointManager : MonoBehaviour
{
    public GameObject player;
    public float knightOffsidePoint;
    public static PointManager instance;
    public List<Points> points;
    public List<EnemyPoints> enemyPoints;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update() {
        GetPoint(PointsNames.Knight).pointA.position = new Vector3(player.transform.position.x - knightOffsidePoint, player.transform.position.y,player.transform.position.z);
        GetPoint(PointsNames.Knight).pointB.position = new Vector3(player.transform.position.x + knightOffsidePoint, player.transform.position.y,player.transform.position.z);
    }
    public Points GetPoint(PointsNames targetPointName)
    {// search point that match with parameter
        foreach (Points currentPoint in points)
        {
            if (currentPoint.pointsNames == targetPointName)
            {
                return currentPoint;
            }
        }
        return points[0];
    }
    public EnemyPoints GetEnemyPoints(EnemyPointNames targetPointName) {
        foreach (EnemyPoints currentPoint in enemyPoints)
        {
            if (currentPoint.enemyPointNames == targetPointName)
            {
                return currentPoint;
            }
        }
        return enemyPoints[0];
    }
    //Sistem ketika Malam
    //sistem eemy
}
