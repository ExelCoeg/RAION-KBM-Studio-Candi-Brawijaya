using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum Status
{
    Poor, Villager, Archer
}
public class NPCAI : MonoBehaviour
{
    [SerializeField]Points points;

    [SerializeField] bool isIdle;
    [SerializeField] bool isNight;//Ketika mengejar sesuatu tidak akan berhenti untuk idle(bisa juga kalau pas malam biar NPC balik ke base tanpa ada idle random)

    [SerializeField]private int movSpeed;
    [SerializeField]private int direction = 1;
    [SerializeField]private float maksIdleTime;
    [SerializeField]private int idleDelay;

    [SerializeField]LayerMask coinLayer;
    [SerializeField]float overLapRadius;
    [SerializeField]Status status = Status.Poor;
    

    float timer;
    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Start()
    {
        ChangeStatus();  
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x == 0){isIdle = true; 
        }else {isIdle = false;};
        timer += Time.deltaTime;
        NPCDirection();
        ChangePoint();
        CheckSurrounding();
    }
    private void FixedUpdate()
    {
        NPCMovement();
    }
    private float RandomNumGen(){
        return UnityEngine.Random.Range(0,5f - maksIdleTime);
    }
    //========================================================================================================================================================
    // pada kode ini nantinya akan mengubah points ssuai dengan status dan akan ada kode untuk menyeimbangkan jumlah NPC pada point tertentur

    public void ChangeStatus(){  
        switch (status){
            case Status.Poor:
                points = PointManager.instance.getPoint(PointsNames.PoorLeft);
                points.NPCCount++;
                Debug.Log(points.NPCCount);
                SetPoint(points.pointA);
                break;
            case Status.Villager:
                points = PointManager.instance.getPoint(PointsNames.Villager);
                points.NPCCount++;
                SetPoint(points.pointA);
                break;
        }
    }
    public void ChangePoint(){
        if (currentPoint == points.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointB);
            StartCoroutine(NPCIdleForSec());
        }else if(currentPoint == points.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointA);
            StartCoroutine(NPCIdleForSec());
        }
    }
    public void SetPoint(Transform transform){
        currentPoint = transform;
    }
    //============================================================-Movement-============================================================================================
    private void NPCMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

        if (timer >= idleDelay)
        {
            Debug.Log("idle");
            StartCoroutine(NPCIdleForSec());
            timer = 0;
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
    IEnumerator NPCIdleForSec(){
        if (rb.velocity.x != 0 && !isNight)
        {
            int movSpeed = this.movSpeed;
            this.movSpeed = 0;
            yield return new WaitForSeconds(RandomNumGen());
            this.movSpeed = movSpeed;
        }
    }
    //========================================================================================================================================================
    private void CheckSurrounding(){
        Collider2D objectInfo = Physics2D.OverlapCircle(transform.position, overLapRadius,coinLayer);
        if (objectInfo != null && objectInfo.tag == "coin")
        {
            SetPoint(objectInfo.transform);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,overLapRadius);
    }
    //========================================================================================================================================================
    private void OnTriggerEnter2D(Collider2D objectInfo) {
        if (objectInfo.tag == "coin")
        {
            Destroy(objectInfo.gameObject);
            SetPoint(points.pointA);
            status = Status.Villager;
            ChangeStatus();
        }
    }

}
// masalah : case jika coin diambil NPC lain. Status harusnya diupdate secara berkala (tetapi ada masalah)