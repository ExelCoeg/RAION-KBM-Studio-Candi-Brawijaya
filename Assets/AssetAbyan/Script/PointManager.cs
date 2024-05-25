using System;
using System.Collections.Generic;
using UnityEngine;

public enum PointsNames
{
    LeftVegrant, RightVegrant, Villager, LeftArcher, RightArcher, Knight
}
[Serializable]
public class Points
{
    public PointsNames pointsNames;
    public Transform pointA, pointB;
    public int NPCCount;
}

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    public List<Points> points;
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
    public Points getPoint(PointsNames targetPointName)
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
    //Sistem ketika Malam
}
