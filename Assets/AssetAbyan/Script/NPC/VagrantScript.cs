using System;
using UnityEngine;

public class VagrantScript : NPC
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] Boolean playerDetected;
    [SerializeField] float vagrantDetection;
    private void Awake() {
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
    }
    private void FixedUpdate() {
        NPCMovement();
    }
    private void SetNPCAtribut(){
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
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position, vagrantDetection, playerLayer);

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
        Gizmos.DrawWireSphere(transform.position, vagrantDetection);
    }

    public override void ChangeStatus(Status status){
       npcManager.InstanceNPC(status, transform.position);
       points.NPCCount--;
       npcManager.vagrantCount--;
       Destroy(gameObject);
    }
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
    }
}
