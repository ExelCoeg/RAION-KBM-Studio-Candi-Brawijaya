using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum Status
{
    Poor, Village, Archer
}
public class NPCAI : MonoBehaviour
{
    [SerializeField]Points points;

    [SerializeField] bool isIdle;

    [SerializeField]private int movSpeed;
    [SerializeField]private int direction = 1;
    [SerializeField]private float maksIdelTime;
    [SerializeField]private int idleFrequency;

    float timer;
    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Start()
    {
        ChangeStatus(Status.Poor);  
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x == 0){isIdle = true; 
        }else {isIdle = false;};
        timer += Time.deltaTime;
        NPCDirection();
        ChangePoint();
    }
    private void FixedUpdate()
    {
        NPCMovement();
    }
    public void button(){
        ChangeStatus(Status.Village);
    }
    public void ChangeStatus(Status status)// pada kode ini nantinya akan mengubah points ssuai dengan status dan akan ada kode untuk menyeimbangkan jumlah NPC pada point tertentur
    {
        switch (status){
            case Status.Poor:points = PointManager.instance.getPoint(PointsNames.PoorLeft);
                currentPoint = points.pointA;
                break;
            case Status.Village:
                points =  PointManager.instance.getPoint(PointsNames.PoorRight);
                currentPoint = points.pointA;
                break;
        }
    }
    public void ChangePoint(){
        if (currentPoint == points.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            currentPoint = points.pointB;
            StartCoroutine(NPCIdleForSec());
            Debug.Log("change");
        }else if(currentPoint == points.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            currentPoint = points.pointA;
            StartCoroutine(NPCIdleForSec());
            
        }
    }
    private void NPCMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);

        if (timer >= idleFrequency && rb.velocity.x != 0)
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
        int movSpeed = this.movSpeed;
        this.movSpeed = 0;
        yield return new WaitForSeconds(RandomNumGen());
        this.movSpeed = movSpeed;
    }
    private float RandomNumGen(){
        return UnityEngine.Random.Range(0,5f - maksIdelTime);
    }
}