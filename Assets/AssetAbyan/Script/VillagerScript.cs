using System;
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
       npcManager.villagerCount--;
       Destroy(gameObject);
    }
    //=======================================================================================================================
    public void VillagerMining(){
        if (!isNight)
        {
            isMining = isIdle;
            miningTimer += Time.deltaTime;
        }else{
            isMining = false;
        }
        if (miningTimer >= npcManager.villagerMiningTimeToCoin)
        {
            Debug.Log("! Coin Collected");
            miningTimer = 0;
        }
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
