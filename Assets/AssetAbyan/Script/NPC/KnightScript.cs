using System;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class KngihtScript : NPC
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Collider2D objectInRange;

    [SerializeField] Boolean enemyDetected;//nanti bakal dipindahin ke player
    [SerializeField] float offSideTimer;
    [SerializeField] float knightDetection;
    
    [Header("Knight")]
    private float attackTimer;
    [SerializeField] Collider2D objectInRangeAttack;
    [SerializeField] private float knightRangeAttack;
    [SerializeField] private float KnightAttackSpeed;
    [SerializeField] private Transform attackPoint;
    [SerializeField] Boolean enemyInRangeAttack;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.Knight);
        SetPoint(points.pointA);
        SetIdle(npcManager.knightIdleSet);
        movSpeed = npcManager.movSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.knightCount++;
        enemyDetected = false;
        offSideTimer = 0;
        status = Status.Knight;
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
        KnightAttack();
    }
    private void FixedUpdate() {
        NPCMovement();
    }
    private void SetNPCAtribut(){
        SetIdle(npcManager.knightIdleSet);
        knightDetection = npcManager.knightDetection;
        knightRangeAttack = npcManager.knightRangeAttack;
        KnightAttackSpeed = npcManager.knightAttackSpeed;
    }
    public void KnightAttack(){
        attackTimer += Time.deltaTime;
        if(enemyInRangeAttack){
            if (attackTimer > npcManager.knightAttackSpeed)
            {
                animator.Play("KnightAttack");
                attackTimer = 0;
            }
        }else if(attackTimer > npcManager.knightAttackSpeed){
            attackTimer = npcManager.knightAttackSpeed;
        }
    }
    private void CheckEnemyInRange(){
        if (!(points.pointA.position.x < transform.position.x && points.pointB.position.x> transform.position.x)){
            if (!enemyInRangeAttack){
                offSideTimer += Time.deltaTime; 
            }
        }else if(offSideTimer > 2){
            offSideTimer = 0;
        }
        if (offSideTimer < 2)
        {
            objectInRange = Physics2D.OverlapCircle(transform.position, knightDetection, enemyLayer); 
        }else{
            objectInRange = null;
        }
        objectInRangeAttack = Physics2D.OverlapCircle(attackPoint.position, knightRangeAttack, enemyLayer);

        if (objectInRange != null && objectInRange.tag == "Enemy"){
            SetPoint(objectInRange.gameObject.transform);
            enemyDetected = true;
            if(!enemyInRangeAttack){
                Idle(false);
            }
        }else if(enemyDetected){
            SetPoint(points.pointA);
            enemyDetected = false;
        }
        
        if (objectInRangeAttack != null && objectInRangeAttack.tag == "Enemy"){
            Idle(true);
            enemyInRangeAttack = true;
        }else if(enemyInRangeAttack){
            enemyInRangeAttack = false; 
            Idle(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, knightDetection);
        Gizmos.DrawWireSphere(attackPoint.position, knightRangeAttack);
    }

    public override void ChangeStatus(Status status){
       npcManager.InstanceNPC(status, transform.position);
       points.NPCCount--;
       npcManager.knightCount--;
       Destroy(gameObject);
    }
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
    }
}
