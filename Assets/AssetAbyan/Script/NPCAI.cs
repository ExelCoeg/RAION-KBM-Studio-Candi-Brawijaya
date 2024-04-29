using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum Status
{
    Vegrant, Villager, Archer, Knight
}
public class NPCAI : MonoBehaviour
{
    [SerializeField] Points points;

    [SerializeField] bool isIdle;
    [SerializeField] bool isNight;//Ketika mengejar sesuatu tidak akan berhenti untuk idle(bisa juga kalau pas malam biar NPC balik ke base tanpa ada idle random)
    [SerializeField] bool c;

    [SerializeField]private int movSpeed;
    [SerializeField]private int direction = 1;
    [SerializeField]private float maksIdleTime;
    [SerializeField]private int idleDelay;
    [SerializeField] float movementTimer;



    [SerializeField]private float hp;
    [SerializeField]private float damage;
    [SerializeField]private float miningProduct;


    [SerializeField]LayerMask coinLayer;
    [SerializeField]float overLapRadius;
    [SerializeField]Status status;
    

    
    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Start()
    {
        ChangeStatus(Status.Vegrant);  
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x == 0){isIdle = true; 
        }else {isIdle = false;}

        NPCDirection();
        ChangePoint();
        CheckSurrounding();

        switch (status)
        {
            case Status.Vegrant:
                break;
            case Status.Villager:
                minig();
                break;
            case Status.Archer:
                break;
            case Status.Knight:
                break;
            default:
                break;
        }
    }
    private void FixedUpdate()
    {
        NPCMovement();
    }
    private float RandomNumGen(float min, float max){
        return UnityEngine.Random.Range(min,max);
    }
    //========================================================================================================================================================
    // pada kode ini nantinya akan mengubah points ssuai dengan status dan akan ada kode untuk menyeimbangkan jumlah NPC pada point tertentur

    public void ChangeStatus(Status status){  
        switch (status){
            case Status.Vegrant:
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.LeftVegrant);// nanti rencananya ada pembagian left sama rightnya
                points.NPCCount++;
                SetPoint(points.pointA);
                break;
            case Status.Villager:
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.Villager);
                points.NPCCount++;
                SetPoint(points.pointA);
                break;
            case Status.Archer:
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.LeftArcher);
                break;
            default:
                break;
        }
    }
    public void ChangePoint(){
        if (currentPoint == points.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointB);
        }else if(currentPoint == points.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointA);
        }
    }
    public void SetPoint(Transform transform){
        currentPoint = transform;
    }
    //============================================================-Movement-============================================================================================
    private void NPCMovement(){
        movementTimer += Time.deltaTime;
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

        if (movementTimer >= RandomNumGen(3,5))
        {
            Debug.Log("idle");
            StartCoroutine(NPCIdleForSec(0.5f,5f));
            movementTimer = 0;
        }

    }
    private void NPCDirection()
    {
        if (transform.position.x > currentPoint.position.x){
            direction = -1;
            }
        else{
            direction = 1;
        }
    }
    IEnumerator NPCIdleForSec(float min, float max){
        if (rb.velocity.x != 0 && !isNight)
        {
            int movSpeed = this.movSpeed;
            this.movSpeed = 0;
            yield return new WaitForSeconds(RandomNumGen(min,max));
            this.movSpeed = movSpeed;
        }
    }
    //===============================================================-HP-=====================================================================================
    public void setHp(float magnitude){
        hp += magnitude;
        if (hp <= 0)
        {
            ChangeStatus(Status.Vegrant);
        }
    }
    //===============================================================-Villager-===============================================================================
    public void minig(){// di update ketika kondisi kita adalah villager
        //panggil animasi
        if (isIdle)
        {
            miningProduct +=Time.deltaTime;
            //panggil objek mining untuk menambah point
            //mengaktifkan mining animation
        }
        if (miningProduct >= 10)//waktu untuk menghasilkan 1 koin 
        {
            //menambah koin sebanyak 1
        }
    }
    //============================================================-Archer====================================================================================

    //========================================================================================================================================================
    private void CheckSurrounding(){
        Collider2D objectInfo = Physics2D.OverlapCircle(transform.position, overLapRadius,coinLayer);
        if (objectInfo != null && objectInfo.tag == "coin")
        {
            //muncul tombol
            //tombol bakal ngerubah status is objekInfo
            SetPoint(objectInfo.transform);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,overLapRadius);
    }
    //========================================================================================================================================================
    private void OnTriggerEnter2D(Collider2D objectInfo) {// nanti ini ada opsi buat pekerjaannya
        if (objectInfo.tag == "coin")
        {
            Destroy(objectInfo.gameObject);
            ChangeStatus(Status.Villager);
        }
    }
    //=======================================================-Animation-======================================================================================



}
// masalah : case jika coin diambil NPC lain. Status harusnya diupdate secara berkala (tetapi ada masalah)