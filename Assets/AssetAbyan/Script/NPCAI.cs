using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
//problem 
//Villager ketika tidak di lokasi mining maka belum ada Idlenya
public enum Status
{
    Vegrant, Villager, Archer, Knight
}
public class NPCAI : MonoBehaviour
{

    private NPCManager npcManager;
    private PointManager pointManager;
    [Header("Status Info")]
    [SerializeField] Status status;
    [SerializeField] int statusInt;
    [SerializeField] Points points;

    [Header("Object Reference")]
    [SerializeField] GameObject bow;//referensi dari objekbow
    [SerializeField] GameObject newBow;//instantiate dari objek bow
    [SerializeField] Collider2D objectInRange;//Musuh yang berada di jarak serangan
    [SerializeField] Collider2D objectDetected;//Musuh yang terdeteksi(terlihat)
    [SerializeField] public Animator animator;
    [Header("Condition")]
    [SerializeField] public bool isIdle;//pada NPC kondisi Idle bisa berbeda tergantung job masing masing
    [SerializeField] private bool enemyInRange;
    [SerializeField] private bool enemyDetected;
    [SerializeField] private bool isMining;//Villager
    [Header("Movement")]
    [SerializeField] private float movementTimer;//timer untuk Idle
    [SerializeField] private int defaultMovSpeed;//Default Movement(supaya tidak perlu membuat temp)
    [SerializeField] private int movSpeed;//MovSpeed saat ini
    [SerializeField] private int direction;//Target
    [SerializeField] private float minIdleDelay;//nilai minimal jeda Sebelum Idle
    [SerializeField] private float maxIdleDelay;//nilai maksimal jeda Sebelum Idle
    [SerializeField] private float minIdleTime;//waktu mininal NPC Idle
    [SerializeField] private float maxIdleTime;//waktu maksimal NPC Idle
    
    [Header("Villager")]
    [SerializeField] private float miningTimer;
    [SerializeField] private float miningTimeToCoin;

    [Header("Scan Surrounding")]
    [SerializeField] public float overLapKnightDetected;
    [SerializeField] float overLapInRange;

    [Header("Scan Surrounding")]
    [SerializeField] private Transform inRangeCheck;//posisi untuk Scan surrounding
    [SerializeField] public float overLapDetected;
    [SerializeField] LayerMask enemyLayer;



    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Start(){
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        npcManager.NPCCount(status,1);
        rb = GetComponent<Rigidbody2D>();
        VegrantSet();
        // ChangeStatus(Status.Vegrant);
        movSpeed = defaultMovSpeed;
        isMining = false;
    }

    private void Update(){
        NPCDirection();
        ChangePoint();
        setAnimationParameter();
        flip();

        switch (status){
            case Status.Vegrant:
                break;
            case Status.Villager:
                Mining();
                break;
            case Status.Archer:
                CheckEnemyDetected();
                //Shoot();
                break;
            case Status.Knight:
                CheckEnemyDetected();
                CheckEnemyInRange();
                KnightAttack();
                break;
            default:
                break;
        }
    }
    private void FixedUpdate(){
        NPCMovement();
    }
    private float RandomNumGen(float min, float max){return UnityEngine.Random.Range(min, max);}
    //========================================================================================================================================================
    // pada kode ini nantinya akan mengubah points ssuai dengan status dan akan ada kode untuk menyeimbangkan jumlah NPC pada point tertentur

    public Points SetPlace(PointsNames left, PointsNames right){
        if (pointManager.getPoint(left).NPCCount <= pointManager.getPoint(right).NPCCount){
            return pointManager.getPoint(left);// nanti rencananya ada pembagian left sama rightnya
        }else{
            return pointManager.getPoint(right);
        }
    }

    public void SetIdle(float[] idleSet){
        minIdleDelay = idleSet[0];
        maxIdleDelay = idleSet[1];
        minIdleTime = idleSet[2];
        maxIdleTime = idleSet[3];
    }

    public void ChangePoint(){
        if (currentPoint == points.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointB);
        }
        else if (currentPoint == points.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointA);
        }
    }
    public void SetPoint(Transform transform){
        currentPoint = transform;//current point adalah poin dari tujuan NPC
    }
    public void DefaultCondition(){
        enemyDetected = false;
        enemyInRange = false;
        isMining = false;
        Idle(false);
    }
    //===============================================================-Vegrant-===============================================================================
    public void VegrantSet(){
        points.NPCCount--;
        npcManager.NPCCount(status,-1);
        this.status = Status.Vegrant;
        statusInt = 0;
        points = SetPlace(PointsNames.LeftVegrant, PointsNames.RightVegrant);
        points.NPCCount++;
        npcManager.NPCCount(status,1);
        SetPoint(points.pointA);
        if (newBow != null)
        {
            Destroy(newBow);
        }
        animator.Play("VegrantIdle");
        DefaultCondition();
    }
    public void VegrantAdjust(){
        SetIdle(npcManager.vegrantIdleSet);
        overLapDetected = npcManager.vegrantDetection;
    }
    
    //===============================================================-Villager-===============================================================================
    public void VillagerSet(){
        points.NPCCount--;
        npcManager.NPCCount(status,-1);
        this.status = Status.Villager;
        statusInt = 1;
        points = pointManager.getPoint(PointsNames.Villager);
        points.NPCCount++;
        npcManager.NPCCount(status,1);
        SetPoint(points.pointA);
        if (newBow != null)
        {
            Destroy(newBow);
        }

        animator.Play("VillagerIdle");
        SetIdle(npcManager.villagerIdleSet);
        overLapDetected = 5;
        DefaultCondition();
    }
    public void Mining(){// di update ketika kondisi kita adalah villager
        //panggil animasi
        if (isIdle){
            miningTimer += Time.deltaTime;// jadi nanti sistem miningnya base on time dia mining
            isMining = true;
        }else{
            isMining = false;
        }
        if (miningTimer >= miningTimeToCoin){//waktu untuk menghasilkan 1 koin 
            Debug.Log("! Coin Collected");
            miningTimer = 0;
        }
    }
    //============================================================-Archer====================================================================================
    public void ArcherSet(){
        points.NPCCount--;
        npcManager.NPCCount(status,-1);
        this.status = Status.Archer;
        statusInt = 2;
        points = pointManager.getPoint(PointsNames.LeftArcher);//nanti rencananya bakal kalkulate
        points.NPCCount++;
        npcManager.NPCCount(status,1);
        SetPoint(points.pointA);

        animator.Play("ArcherIdle");
        if (newBow == null){
            newBow = Instantiate(bow, transform.position, Quaternion.identity);
            newBow.transform.parent = transform;
            newBow.transform.localScale = bow.transform.localScale;
            newBow.GetComponent<Bow>().parent = gameObject;
        }
        SetIdle(npcManager.archerIdleSet);
        DefaultCondition();
    }
    //============================================================-Knight-====================================================================================
    public void KnightSet(){
        points.NPCCount--;
        npcManager.NPCCount(status,-1);
        this.status = Status.Knight;
        statusInt = 3;
        points = pointManager.getPoint(PointsNames.Knight);//nanti rencananya bakal kalkulate
        points.NPCCount++;
        npcManager.NPCCount(status,1);
        SetPoint(points.pointA);
        if (newBow != null)
        {
            Destroy(newBow);
        }
        animator.Play("KnightIdle");
        SetIdle(npcManager.knigtIdleSet);
        overLapDetected = overLapKnightDetected;
        overLapDetected = 20;
        DefaultCondition();
    }
    public void KnightAttack(){

    }
    //============================================================-Movement-============================================================================================
    private void NPCMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

        if (points.pointA.position.x > transform.position.x || points.pointB.position.x < transform.position.x){
            return;//Ketika NPC belum sampai tujuan(diantara poit A dan B) maka NPC akan terus berjalan tanpa idle
        }else if(enemyDetected){
            return;
        }

        movementTimer += Time.deltaTime;
        if (movementTimer >= RandomNumGen(npcManager.villagerIdleSet[0], maxIdleDelay)){
            StartCoroutine(NPCIdleForSec(RandomNumGen(minIdleTime, maxIdleTime)));
            movementTimer = 0;
        }else if (isIdle){
            movementTimer = 0;
        }

    }
    private void NPCDirection(){
        if (currentPoint != null){
            if (transform.position.x > currentPoint.position.x){
                direction = -1;
            }
            else{
                direction = 1;
            }
        }
        
    }
    public void Idle(Boolean isIdle){
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = defaultMovSpeed;
        }
    }
    IEnumerator NPCIdleForSec(float idleTime){
        if (rb.velocity.x != 0){
            Idle(true);
            yield return new WaitForSeconds(idleTime);
            Idle(false);
        }
    }
    //========================================================================================================================================================
    private void CheckEnemyDetected(){
        objectDetected = Physics2D.OverlapCircle(transform.position, overLapDetected, enemyLayer);

        if (objectDetected != null && objectDetected.tag == "Enemy"){
            SetPoint(objectDetected.transform);
            Debug.Log(currentPoint.name);
            enemyDetected = true;
        }else if(enemyDetected){
            currentPoint = points.pointA;
            enemyDetected = false;
        }

    }
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(inRangeCheck.position, overLapInRange, enemyLayer);

        if (objectInRange != null && objectInRange.tag == "Enemy"){
            Idle(true);
            enemyInRange = true;
        }else if(enemyInRange){
            Idle(false);
            enemyInRange = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, overLapDetected);
        Gizmos.DrawWireSphere(inRangeCheck.position, overLapInRange);
    }
    //========================================================================================================================================================
    private void OnTriggerEnter2D(Collider2D objectInfo){// nanti ini ada opsi buat pekerjaannya
        if (objectInfo.tag == "Coin"){
            Destroy(objectInfo.gameObject);
            VillagerSet();
        }
        if (objectInfo.tag == "Bow"){
            Destroy(objectInfo.gameObject);
            ArcherSet();
        }
        if (objectInfo.tag == "Sword"){
            Destroy(objectInfo.gameObject);
            KnightSet();
            
        }
    }
    //=======================================================-Animatio  ==============================================================

    private void setAnimationParameter(){
        animator.SetFloat("movSpeed", Mathf.Abs(movSpeed));
        animator.SetInteger("statusInt", statusInt);
        animator.SetBool("isMining", isMining);
        animator.SetBool("enemyInRange", enemyInRange);
    }
    private void flip(){
        Vector3 scale = transform.localScale;
        if (direction == -1 || direction == 1){
            scale.x = direction;
        }
        transform.localScale = scale;
    }


}
// masalah : case jika coin diambil NPC lain. Status harusnya diupdate secara berkala (tetapi ada masalah)