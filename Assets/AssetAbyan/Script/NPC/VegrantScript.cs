using System;
using UnityEngine;

public class VegrantScript : NPC
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] Boolean playerDetected;
    [SerializeField] float vegrantDetection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.LeftVegrant, PointsNames.RightVegrant);
        SetPoint(points.pointA);
        SetIdle(npcManager.vegrantIdleSet);
        movSpeed = npcManager.movSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.vegrantCount++;
        playerDetected = false;
        status = Status.Vegrant;
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
        SetIdle(npcManager.vegrantIdleSet);
        vegrantDetection = npcManager.vegrantDetection;
    }
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position, vegrantDetection, playerLayer);

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
        Gizmos.DrawWireSphere(transform.position, vegrantDetection);
    }

    public override void ChangeStatus(Status status){
       npcManager.InstanceNPC(status, transform.position);
       points.NPCCount--;
       npcManager.vegrantCount--;
       Destroy(gameObject);
    }
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
    }
}
