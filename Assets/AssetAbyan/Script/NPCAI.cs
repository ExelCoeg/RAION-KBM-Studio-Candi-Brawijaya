using System;
using System.Collections;
using System.Data.Common;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
//problem 
//Villager ketika tidak di lokasi mining maka belum ada Idlenya
public enum Status
{
    Vegrant, Villager, Archer, Knight
}
public class NPCAI : MonoBehaviour
{
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
    [SerializeField] private Transform inRangeCheck;//posisi untuk Scan surrounding
    [SerializeField] float overLapDetected;
    [SerializeField] float overLapInRange;
    [SerializeField] LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        ChangeStatus(Status.Vegrant);
        movSpeed = defaultMovSpeed;
        isMining = false;
    }

    private void Update(){
        NPCDirection();
        ChangePoint();
        CheckSurrounding();
        setAnimationParameter();
        flip();

        switch (status){
            case Status.Vegrant:
                break;
            case Status.Villager:
                Mining();
                break;
            case Status.Archer:
                //Shoot();
                break;
            case Status.Knight:
                KnightAttack();
                break;
            default:
                break;
        }
    }
    private void FixedUpdate(){
        NPCMovement();
    }
    private float RandomNumGen(float min, float max){return UnityEngine.Random.Range(min, max);
    }
    //========================================================================================================================================================
    // pada kode ini nantinya akan mengubah points ssuai dengan status dan akan ada kode untuk menyeimbangkan jumlah NPC pada point tertentur

    public Points SetPlace(PointsNames left, PointsNames right){
        if (PointManager.instance.getPoint(left).NPCCount <= PointManager.instance.getPoint(right).NPCCount){
            return PointManager.instance.getPoint(left);// nanti rencananya ada pembagian left sama rightnya
        }else{
            return PointManager.instance.getPoint(right);
        }
    }

    public void SetIdle(float minDelay,float maxDelay,float minTime,float maxTime){
        minIdleDelay = minDelay;
        maxIdleDelay = maxDelay;
        minIdleTime = minTime;
        maxIdleTime = maxTime;
    }

    public void ChangeStatus(Status status){
        switch (status){
            case Status.Vegrant:
                animator.Play("VegrantIdle");
                points.NPCCount--;
                this.status = status;
                points = SetPlace(PointsNames.LeftVegrant,PointsNames.RightVegrant);
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow != null){
                    Destroy(newBow);
                }
                SetIdle(3,5,1,2);
                statusInt = 0;
                break;
            case Status.Villager:
                animator.Play("VillagerIdle");
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.Villager);
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow != null){
                    Destroy(newBow);
                }
                SetIdle(1,2,5,10);
                statusInt = 1;
                break;
            case Status.Archer:
                animator.Play("ArcherIdle");
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.LeftArcher);//nanti rencananya bakal kalkulate
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow == null){
                    newBow = Instantiate(bow, transform.position, Quaternion.identity);
                    newBow.transform.parent = transform;
                }
                SetIdle(3,5,1,2);
                statusInt = 2;
                break;
            case Status.Knight:
                animator.Play("KnightIdle");
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.Knight);//nanti rencananya bakal kalkulate
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow != null){
                    Destroy(newBow);
                }
                SetIdle(3,5,1,2);
                statusInt = 3;
                break;
            default:
                break;
        }
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
    //============================================================-Movement-============================================================================================
    private void NPCMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

        if (points.pointA.position.x > transform.position.x || points.pointB.position.x < transform.position.x){
            return;//Ketika NPC belum sampai tujuan(diantara poit A dan B) maka NPC akan terus berjalan tanpa idle
        }else if(enemyDetected){
            return;
        }

        movementTimer += Time.deltaTime;
        if (movementTimer >= RandomNumGen(minIdleDelay, maxIdleDelay)){
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
    //===============================================================-Villager-===============================================================================
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

    }
    //============================================================-Knight-====================================================================================
    public void KnightAttack(){

    }
    //========================================================================================================================================================
    private void CheckSurrounding(){
        objectDetected = Physics2D.OverlapCircle(transform.position, overLapDetected, enemyLayer);
        objectInRange = Physics2D.OverlapCircle(inRangeCheck.position, overLapInRange, enemyLayer);

        if (objectDetected != null && objectDetected.tag == "Enemy"){
            SetPoint(objectDetected.transform);
            enemyDetected = true;
        }else if(enemyDetected){
            currentPoint = points.pointA;
            enemyDetected = false;
        }

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
            ChangeStatus(Status.Villager);
        }
        if (objectInfo.tag == "Bow"){
            Destroy(objectInfo.gameObject);
            ChangeStatus(Status.Archer);
        }
        if (objectInfo.tag == "Sword"){
            Destroy(objectInfo.gameObject);
            ChangeStatus(Status.Knight);
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