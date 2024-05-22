
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    [Header("Vegrant")]
    public int vegrantCount = 0;
    public float vegrantDetection = 0;
    public float[] vegrantIdleSet = new float[4];
    [Header("Villager")]
    public int villagerCount = 0;
    public float villagerDetection = 0;
    public float[] villagerIdleSet = new float[4];
    public float villagerMiningTimeToCoin = 0;
    [Header("Archer")]
    public  int archerCount = 0;
    public float archerDetection = 0;
    public float[] archerIdleSet = new float[4];
    public float archerDamage = 0;
    public float archerShootFrequency;//setiap berapa detik?
    [Header("Knight")]
    public int knightCount = 0;
    public float knightDetection = 0;
    public float[] knigtIdleSet = new float[4];
    public float knightDamage = 0;
    public float KnightAttackFrequency = 0;
    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }
    public void NPCCount(Status status,int i){
        Debug.Log(status);
        switch(status){
            case Status.Vegrant: 
                vegrantCount += i;
                if (vegrantCount <= 0){vegrantCount = 0;}
            ; break;
            case Status.Villager: 
                villagerCount += i;
                if (villagerCount <= 0){villagerCount = 0;}
            ; break;
            case Status.Archer: 
                archerCount += i; 
                if (archerCount <= 0){archerCount = 0;}
            ; break;
            case Status.Knight: 
                knightCount += i;
                if (knightCount <= 0){knightCount = 0;}
            ; break;
            default: break;
        }
        
    }

}
