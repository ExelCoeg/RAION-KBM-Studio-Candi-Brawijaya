using System;
using UnityEngine;
public class VagrantScript : NPC
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] Boolean playerDetected;
    [SerializeField] float vagrantDetection;
    [SerializeField] float offSideTimer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.LeftVagrant, PointsNames.RightVagrant);
        SetPoint(points.pointA);
        SetIdle(npcManager.vagrantIdleSet);
        movSpeed = npcManager.vagrantMovSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.vagrantCount++;
        playerDetected = false;
        status = Status.Vagrant;
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
        // RandomSpawn();

    }
    private void FixedUpdate()
    {
        NPCMovement();
    }

    // public void RandomSpawn(){
    //     if(RandomNumGen(0,2) == 2){
    //         NPCManager.instance.InstanceNPC(Status.Vagrant, new Vector2(RandomNumGen(TerritoryManager.instance.pointAx, TerritoryManager.instance.pointBx), transform.position.y));
    //     }
    // }
    public override Points SetPlace(PointsNames left, PointsNames right){
        if (transform.position.x <= 0){
            return pointManager.GetPoint(left);
        }else{
            return pointManager.GetPoint(right);
        }
    }
    private void SetNPCAtribut()
    {
        SetIdle(npcManager.vagrantIdleSet);
        vagrantDetection = npcManager.vagrantDetection;
    }
     public override void Idle(Boolean isIdle){
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = npcManager.vagrantMovSpeed;
        }
    }

    
    private void CheckEnemyInRange()
    {
        // objectInRange = Physics2D.OverlapCircle(transform.position, vagrantDetection, playerLayer);
        if (!(points.pointA.position.x < transform.position.x && points.pointB.position.x> transform.position.x)){
        if (objectInRange){
            offSideTimer += Time.deltaTime; 
        }   
        }else if(offSideTimer > 2){
            offSideTimer = 0;
        }
        if (offSideTimer < 2)
        {
            objectInRange = Physics2D.OverlapCircle(transform.position,  vagrantDetection, playerLayer); 
        }
        else{
            objectInRange = null;
        }
        if (objectInRange != null && objectInRange.tag == "Player")
        {
            float distance = Mathf.Abs(objectInRange.transform.position.x - transform.position.x);
            if (distance <= 1f)
            {
                Idle(true);
            }
            else
            {
                Idle(false);
                SetPoint(objectInRange.gameObject.transform);
                playerDetected = true;
            }
        }
        else if (playerDetected)
        {
            SetPoint(points.pointA);
            playerDetected = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, vagrantDetection);
    }

    public override void ChangeStatus(Status status)
    {
        npcManager.InstanceNPC(status, transform.position);
        points.NPCCount--;
        npcManager.vagrantCount--;
        Destroy(gameObject);
    }
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
    }
}
