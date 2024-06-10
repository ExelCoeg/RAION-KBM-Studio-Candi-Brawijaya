using System;
using System.Collections.Generic;
using UnityEngine;

public enum PointsNames
{
    LeftVagrant, RightVagrant, Villager, LeftArcher, RightArcher, Knight, VillagerDefault
}
public enum EnemyPointNames{
    Left,Right, LeftDefault, RightDefault
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
        if(!DayManager.instance.isNight){
            //Left Archer
            GetPoint(PointsNames.LeftArcher).pointA.position = TerritoryManager.instance.territoryPoints.pointA.position;
            GetPoint(PointsNames.LeftArcher).pointB.position = Vector2.zero;


            //Right Archer
            GetPoint(PointsNames.RightArcher).pointA.position = Vector2.zero;
            GetPoint(PointsNames.RightArcher).pointB.position = TerritoryManager.instance.territoryPoints.pointB.position;


            //Villager
            GetPoint(PointsNames.Villager).pointA.position = TerritoryManager.instance.territoryPoints.pointA.position;
            GetPoint(PointsNames.Villager).pointB.position = TerritoryManager.instance.territoryPoints.pointB.position;
        
            //Enemy
            GetEnemyPoints(EnemyPointNames.Left).pointB.position = Vector3.Lerp(GetEnemyPoints(EnemyPointNames.Left).pointB.position, Vector2.zero,DayManager.instance.getTimeToDay());
            GetEnemyPoints(EnemyPointNames.Right).pointA.position =  Vector3.Lerp(GetEnemyPoints(EnemyPointNames.Right).pointB.position, Vector2.zero,DayManager.instance.getTimeToDay());

        }       
        else{
            
            GetPoint(PointsNames.LeftArcher).pointB.position = new Vector2(TerritoryManager.instance.pointAx + 10, 0);
            GetPoint(PointsNames.RightArcher).pointA.position = new Vector2(TerritoryManager.instance.pointBx - 10, 0);
            
            GetPoint(PointsNames.Villager).pointA.position = GetPoint(PointsNames.VillagerDefault).pointA.position;
            GetPoint(PointsNames.Villager).pointB.position = GetPoint(PointsNames.VillagerDefault).pointB.position;  
            GetEnemyPoints(EnemyPointNames.Left).pointB.position = TerritoryManager.instance.territoryPoints.pointA.position;
            GetEnemyPoints(EnemyPointNames.Right).pointA.position = TerritoryManager.instance.territoryPoints.pointB.position;

        }
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
