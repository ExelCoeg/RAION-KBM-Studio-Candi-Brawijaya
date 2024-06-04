using System;
using UnityEngine;

public enum EnemyStatus
{
    Lemulut, Gelapari, Widara
}
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Lemulut")]
    public GameObject lemulut;
    public int lemulutCount = 0;
    public float lemulutMovSpeed = 0;
    public float lemulutDamage = 0;
    public float lemulutAttackSpeed = 0;
    public float lemulutHealth;
    public float lemulutDetection = 0;
    public float lemulutRangeAttack = 0;
    public String[] lelumutDamageAble;
    public float[] lemulutIdleSet = new float[4];//0&1 = lama dia jalan ; 2&3 = lama dia 
    [Header("Gelapari")]
    public GameObject gelapari;
    public int gelapariCount = 0;
    public float gelapariMovSpeed = 0;
    public float gelapariDamage = 0;
    public float gelapariAttackSpeed = 0;
    public float gelapariHealth;
    public float gelapariDetection = 0;
    public float gelapariRangeAttack = 0;
    public String[] gelapariDamageAble;
    public float[] gelapariIdleSet = new float[4];//0&1 = lama dia jalan ; 2&3 = lama dia 
    [Header("Widara")]
    public GameObject widara;
    public int widaraCount = 0;
    public float widaraMovSpeed = 0;
    public float widaraDamage = 0;
    public float widaraAttackSpeed = 0;
    public float widaraHealth;
    public float widaraDetection = 0;
    public float widaraRangeAttack = 0;
    public String[] widaraDamageAble;
    public float[] widaraIdleSet = new float[4];//0&1 = lama dia jalan ; 2&3 = lama dia 

    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }
    private void Start(){
        //InstanceNPC(Status.Vegrant, transform.position);
    }
    public void InstanceEnemy(EnemyStatus enemyStatus,Vector3 position){
        switch (enemyStatus){
            case EnemyStatus.Lemulut:
                Instantiate(lemulut, position, Quaternion.identity);
                break;
            case EnemyStatus.Gelapari:
                Instantiate(gelapari, position, Quaternion.identity);
                break;
            case EnemyStatus.Widara:
                Instantiate(widara, position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
