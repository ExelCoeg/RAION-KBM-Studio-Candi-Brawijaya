using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public enum Status
{
    Vegrant, Villager, Archer, Knight
}
public class NPCAI : MonoBehaviour
{
    [SerializeField] Points points;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject newBow;
    [SerializeField] Collider2D objectInRange;
 
    [SerializeField] bool isIdle;
    [SerializeField] bool isNight;//Ketika mengejar sesuatu tidak akan berhenti untuk idle(bisa juga kalau pas malam biar NPC balik ke base tanpa ada idle random)
    public bool enemyInRange;

    [SerializeField]private int movSpeed;
    [SerializeField]private int defaultMovSpeed;
    [SerializeField]private int direction = 1;
    [SerializeField]private float maksIdleTime;
    [SerializeField]private int idleDelay;
    [SerializeField]float movementTimer;
    [SerializeField]float attackTimer;
    [SerializeField]float attackDelay;

    [SerializeField]private float hp;
    [SerializeField]private float damage;
    [SerializeField]private float miningProduct;


    [SerializeField]LayerMask enemyLayer;
    [SerializeField]float overLapRadius;
    [SerializeField]Status status;
    
    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go
    private void Awake() {
        
    }

    private void Start()
    {
        movSpeed = defaultMovSpeed;
        ChangeStatus(Status.Vegrant);  
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // if (rb.velocity.x == 0){isIdle = true; 
        // }else {isIdle = false;}

        NPCDirection();
        ChangePoint();
        CheckSurrounding();

        switch (status)
        {
            case Status.Vegrant:
                break;
            case Status.Villager:
                mining();
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
                if (PointManager.instance.getPoint(PointsNames.LeftVegrant).NPCCount <= PointManager.instance.getPoint(PointsNames.RightVegrant).NPCCount)
                {
                    points = PointManager.instance.getPoint(PointsNames.LeftVegrant);// nanti rencananya ada pembagian left sama rightnya
                }else{
                    points = PointManager.instance.getPoint(PointsNames.RightVegrant);
                }
                points.NPCCount++;
                
                SetPoint(points.pointA);
                if (newBow != null)
                {
                    Destroy(newBow);   
                }
                break;
            case Status.Villager:
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.Villager);
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow != null)
                {
                    Destroy(newBow);   
                }
                break;
            case Status.Archer:
                points.NPCCount--;
                this.status = status;
                points = PointManager.instance.getPoint(PointsNames.LeftArcher);//nanti rencananya bakal kalkulate
                points.NPCCount++;
                SetPoint(points.pointA);
                if (newBow == null)
                {
                    newBow = Instantiate(bow, transform.position, Quaternion.identity);
                    newBow.transform.parent = transform;
                }
                break;
            case Status.Knight:
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
            StartCoroutine(NPCIdleForSec(1f,3f));
            movementTimer = 0;
        }else if(isIdle){
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
    public void Idle(Boolean isIdle){
        if (isIdle)
        {
            this.isIdle = true;
            movSpeed = 0;
        }else{
            this.isIdle = false;
            movSpeed = defaultMovSpeed;
        }
    }
    IEnumerator NPCIdleForSec(float min, float max){
        if (rb.velocity.x != 0 && !isNight)
        {
            Idle(true);
            yield return new WaitForSeconds(RandomNumGen(min,max));
            Idle(false);
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
    public void mining(){// di update ketika kondisi kita adalah villager
        //panggil animasi
        if (isIdle)
        {
            miningProduct +=Time.deltaTime;// jadi nanti sistem miningnya base on time dia mining
            //panggil objek mining untuk menambah point
            //mengaktifkan mining animation
        }
        if (miningProduct >= 10)//waktu untuk menghasilkan 1 koin 
        {
            //menambah koin sebanyak 1
        }
    }
    //============================================================-Archer====================================================================================
    public void archerSet(){
        
    }
    //============================================================-Knight-====================================================================================
    public void knightSet(){
        
    }
    public void KnightAttack(){
        attackTimer = Time.deltaTime;
        if (enemyInRange && attackTimer >= attackDelay)
        {
            Destroy(objectInRange.gameObject);
        }else if(attackTimer >= attackDelay){
            attackTimer = attackDelay;
        }
    }
    //========================================================================================================================================================
    private void CheckSurrounding(){
        objectInRange = Physics2D.OverlapCircle(transform.position, overLapRadius,enemyLayer);
        //Debug.Log(objectInRange.name);
        if (objectInRange != null && objectInRange.tag == "Enemy")
        {  
            enemyInRange = true;
        }else{
            enemyInRange = false;
        }    
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,overLapRadius);
    }
    //========================================================================================================================================================
    private void OnTriggerEnter2D(Collider2D objectInfo) {// nanti ini ada opsi buat pekerjaannya
        if (objectInfo.tag == "Coin")
        {
            Destroy(objectInfo.gameObject);
            ChangeStatus(Status.Villager);
        }
        if (objectInfo.tag == "Bow")
        {
            Destroy(objectInfo.gameObject);
            ChangeStatus(Status.Archer);
        }
    }
    //=======================================================-Animation-======================================================================================



}
// masalah : case jika coin diambil NPC lain. Status harusnya diupdate secara berkala (tetapi ada masalah)