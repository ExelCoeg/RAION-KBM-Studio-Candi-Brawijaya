using System;
using UnityEngine;

public enum Status
{
    Vagrant, Villager, Archer, Knight
}
public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    [Header("Vagrant")]
    public GameObject vagrant;
    public int vagrantCount = 0;
    public float vagrantMovSpeed = 3;
    public float vagrantHealth;
    public float vagrantDetection = 0;
    public String[] vagrantDamageAble;
    public float[] vagrantIdleSet = new float[4];
    [Header("Villager")]
    public GameObject villager;
    public int villagerCount = 0;
    public float villagerMovSpeed = 3;
    public float villagerHealth;
    public float villagerDetection = 0;
    public float villagerMiningTimeToCoin = 0;
    public String[] villagerDamageAble;
    public float[] villagerIdleSet = new float[4];
    [Header("Archer")]
    public GameObject archer;
    public  int archerCount = 0;
    public float ArcherMovSpeed = 3;
    public float archerHealth;
    public float archerDetection = 0;
    public float archerDamage = 0;
    public String[] archerDamageAble;
    public float archerAttackSpeed;//setiap berapa detik?
    public float[] archerIdleSet = new float[4];
    [Header("Knight")]
    public GameObject knight;
    public int knightCount = 0;
    public float kngihtMovSpeed = 3;
    public float knightHealth;
    public float knightDetection = 0;
    public float knightRangeAttack = 0;
    public float knightOffSiteRange = 0;//jarak knight tidak mengejar enemy
    public float knightDamage = 0;
    public float knightAttackSpeed = 0;
    public String[] knightDamageAble;
    public float[] knightIdleSet = new float[4];
    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }
    private void Start(){
        //InstanceNPC(Status.Vagrant, transform.position);
    }
    public void InstanceNPC(Status status,Vector3 position){
        switch (status){
            case Status.Vagrant:
                Instantiate(vagrant, position, Quaternion.identity);
                break;
            case Status.Villager:
                Instantiate(villager, position, Quaternion.identity);
                break;
            case Status.Archer:
                Instantiate(archer, position, Quaternion.identity);
                break;
            case Status.Knight:
                Instantiate(knight, position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
    public int getTax(){
        return villagerCount + archerCount + knightCount;
    }
}
