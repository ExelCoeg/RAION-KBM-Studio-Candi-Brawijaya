using UnityEngine;

public enum Status
{
    Vegrant, Villager, Archer, Knight
}
public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    [Header("NPC defoult value")]
    public float movSpeed = 3;

    [Header("Vegrant")]
    public GameObject vegrant;
    public float vegrantHealth;
    public int vegrantCount = 0;
    public float vegrantDetection = 0;
    public float[] vegrantIdleSet = new float[4];
    [Header("Villager")]
    public GameObject villager;
    public float villagerHealth;
    public int villagerCount = 0;
    public float villagerDetection = 0;
    public float villagerMiningTimeToCoin = 0;
    public float[] villagerIdleSet = new float[4];
    [Header("Archer")]
    public GameObject archer;
    public float archerHealth;
    public  int archerCount = 0;
    public float archerDetection = 0;
    public float[] archerIdleSet = new float[4];
    public float archerDamage = 0;
    public float archerAttackSpeed;//setiap berapa detik?
    [Header("Knight")]
    public GameObject knight;
    public float knightHealth;
    public int knightCount = 0;
    public float knightDetection = 0;
    public float knightRangeAttack = 0;
    public float knightOffSiteRange = 0;//jarak knight tidak mengejar enemy
    public float[] knightIdleSet = new float[4];
    public float knightDamage = 0;
    public float knightAttackSpeed = 0;
    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }
    private void Start(){
        //InstanceNPC(Status.Vegrant, transform.position);
    }
    public void InstanceNPC(Status status,Vector3 position){
        switch (status){
            case Status.Vegrant:
                Instantiate(vegrant, position, Quaternion.identity);
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
