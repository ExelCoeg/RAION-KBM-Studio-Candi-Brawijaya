using System;
using System.Linq;
using UnityEngine;

public class ArcherScript : NPC
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] NPCHealth npcHealth;
    [SerializeField] float  archerDetection;

    [SerializeField] Boolean enemyDetected;
    [Header("Bow")]
    [SerializeField]private Transform shootPoint;
    [SerializeField]private GameObject arrow;
    [SerializeField]private Rigidbody2D arrowRb;
    [SerializeField]private Transform enemy;
    [SerializeField]private Transform bowTransform;//virtual bow
    [SerializeField]private float timer;
    [SerializeField]private float angle;
    [SerializeField]private float vo;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        arrowRb = arrow.GetComponent<Rigidbody2D>();
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.LeftArcher,PointsNames.RightArcher);
        SetPoint(points.pointA);
        SetIdle(npcManager.archerIdleSet);
        movSpeed = npcManager.ArcherMovSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.archerCount++;
        enemyDetected = false;
        status = Status.Archer;


        npcHealth = this.GetComponent<NPCHealth>();
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
        Shoot();
        angle = CalculateAngle();
        bowTransform.rotation = Quaternion.Euler(0, 0, angle);
        bowTransform.localScale = new Vector3(direction,bowTransform.localScale.y,bowTransform.localScale.z);// nanti ketika ada animasi shootpoint akan diatur secara manual

    }
    private void FixedUpdate() {
        NPCMovement();
    }
    //=======================================================================================================================
    private void SetNPCAtribut(){
        SetIdle(npcManager.archerIdleSet);
        archerDetection = npcManager.archerDetection - 2;
        vo = CalculatePower(npcManager.archerDetection);

        if (npcHealth != null) {}
        {
            npcHealth.maxHealth = (int)npcManager.archerHealth;   
        }
    }
    public override void Idle(Boolean isIdle){
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = npcManager.ArcherMovSpeed;
        }
    }

    public override void ChangeStatus(Status status){
       npcManager.InstanceNPC(status, transform.position);
       points.NPCCount--;
       npcManager.archerCount--;
       Destroy(gameObject);
    }
    //=======================================================================================================================
    public void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > npcManager.archerAttackSpeed && enemyDetected)
        {
            animator.Play("ArcherShoot");
            timer = 0;
        }else if(!enemyDetected){
            timer = 0;
        }
    }
    public override void Attack(){
        GameObject newArrow = Instantiate(arrow, shootPoint.position, Quaternion.Euler(0, 0, angle));
        newArrow.GetComponent<ArrowScript>().vo = vo;
        newArrow.GetComponent<ArrowScript>().damage = npcManager.archerDamage;
    }
    public float CalculateAngle()
    {
        if (objectInRange && enemy != null)
        {
            Vector2 direction = (enemy.position + new Vector3(0,-1.5f,0)) - transform.position;
            float sinRadianAngle = (10f * arrowRb.gravityScale) * direction.x / (vo * vo);
            float radianAngle2 = Mathf.Asin(sinRadianAngle);
            float angleJustX = (radianAngle2 * Mathf.Rad2Deg) / 2;

            //menambahkan anggle jika sudut y != 0
            float angleRadians = Mathf.Atan2(direction.y, direction.x);
            float angleInDegrees = angleRadians * Mathf.Rad2Deg;

            float angle = angleJustX + angleInDegrees;
            if (float.IsNaN(angle))
            {
                return 0;
            }else{
                return angle;
            }
        }
        return 0;
    }
    public float CalculatePower(float distance){
        return Mathf.Sqrt(archerDetection * 10f * arrowRb.gravityScale);
    }
    //=======================================================================================================================
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position, archerDetection, enemyLayer);

        if (objectInRange != null && npcManager.archerDamageAble.Contains(objectInRange.tag)){
            Idle(true);
            SetPoint(objectInRange.gameObject.transform);
            enemy = objectInRange.transform;
            enemyDetected = true;
        }else if(enemyDetected){
            SetPoint(points.pointA);
            enemy = null;
            enemyDetected = false;
            Idle(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, archerDetection);
    }
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
        animator.SetBool("enemyDetected", enemyDetected);
    }
}
