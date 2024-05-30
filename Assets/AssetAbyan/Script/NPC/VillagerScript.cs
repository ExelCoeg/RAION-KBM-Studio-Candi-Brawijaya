using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VillagerScript : NPC
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] float villagerDetection;

    [SerializeField] Boolean playerDetected;
    [Header("Villager")]
    [SerializeField] float miningTimer;
    [SerializeField] Boolean isMining;
    [SerializeField] int coin;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.Villager);
        SetPoint(points.pointA);
        SetIdle(npcManager.villagerIdleSet);
        movSpeed = npcManager.movSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.villagerCount++;
        playerDetected = false;
        status = Status.Villager;
    }

    // Update is called once per frame
    void Update()
    {
        SetNPCAtribut();
        NPCDirection();
        ChangePoint();
        flip();
        CheckEnemyInRange();
        RandomIdle();
        setAnimationParameter();
        VillagerMining();
    }
    private void FixedUpdate() {
        NPCMovement();
    }
    //=======================================================================================================================
    private void SetNPCAtribut(){
        SetIdle(npcManager.villagerIdleSet);
        villagerDetection = npcManager.villagerDetection;
    }

    public override void ChangeStatus(Status status){
       npcManager.InstanceNPC(status, transform.position);
       points.NPCCount--;
       npcManager.villagerCount--;
       Destroy(gameObject);
    }
    //=======================================================================================================================
    public void VillagerMining(){
        if (!isNight)
        {
            isMining = isIdle;
        }else{
            isMining = false;//tidak dapat minig jika malam hari
        }
        if (isMining)
        {
            miningTimer += Time.deltaTime;
        }else{
            isMining = false;
        }
        if (miningTimer >= npcManager.villagerMiningTimeToCoin)
        {
            coin++;
            miningTimer = 0;
        }
    }
    public int CoinPeek(){
        return coin;
    }
    public int getCoin(){
        int coinTemp = coin;
        coin =0;
        return coinTemp;
    }

    //=======================================================================================================================
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position, villagerDetection, playerLayer);

        if (objectInRange != null && objectInRange.tag == "Player"){
            Idle(false);
            SetPoint(objectInRange.gameObject.transform);
            playerDetected = true;
        }else if(playerDetected){
            SetPoint(points.pointA);
            playerDetected = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, villagerDetection);
    }
    
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
        animator.SetBool("isMining",isMining);
    }
}
