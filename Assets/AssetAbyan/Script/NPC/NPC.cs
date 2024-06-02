using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public Status status;
    [SerializeField] public Animator animator;
    protected NPCManager npcManager;
    protected PointManager pointManager;
    [SerializeField]protected Rigidbody2D rb;
    [SerializeField]protected Transform currentPoint;
    [SerializeField] protected Points points;

    [Header("Condition")]
    [SerializeField] protected bool isIdle;//pada NPC kondisi Idle bisa berbeda tergantung job masing masing
    [SerializeField] protected bool isNight;
    [Header("Atribut")]
    protected float movementTimer;//timer untuk Idle
    [SerializeField] protected float movSpeed;//MovSpeed saat ini
    [SerializeField] protected int direction;//Target
    [SerializeField] private float minIdleDelay;//nilai minimal jeda Sebelum Idle
    [SerializeField] private float maxIdleDelay;//nilai maksimal jeda Sebelum Idle
    [SerializeField] private float minIdleTime;//waktu mininal NPC Idle
    [SerializeField] private float maxIdleTime;//waktu maksimal NPC Idle

    //========================================================================================================================================================
    public void ChangePoint(){
        if (currentPoint == points.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointB);
        }
        else if (currentPoint == points.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5){
            SetPoint(points.pointA);
        }
    }
    public Points SetPlace(PointsNames left, PointsNames right){
        if (pointManager.GetPoint(left).NPCCount <= pointManager.GetPoint(right).NPCCount){
            return pointManager.GetPoint(left);// nanti rencananya ada pembagian left sama rightnya
        }else{
            return pointManager.GetPoint(right);
        }
    }
    public Points SetPlace(PointsNames point){
        return pointManager.GetPoint(point);
    }
    public void SetPoint(Transform transform){
        currentPoint = transform;//current point adalah poin dari tujuan NPC
    }
    public void SetIdle(float[] idleSet){
        minIdleDelay = idleSet[0];
        maxIdleDelay = idleSet[1];
        minIdleTime = idleSet[2];
        maxIdleTime = idleSet[3];
    }
    //========================================================================================================================================================
    protected float RandomNumGen(float min, float max){return UnityEngine.Random.Range(min, max);}
    //========================================================================================================================================================
    protected void NPCMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);
    }
    protected void RandomIdle(){
        if (points.pointA.position.x < transform.position.x && points.pointB.position.x > transform.position.x){
            movementTimer += Time.deltaTime;
            if (movementTimer >= RandomNumGen(minIdleDelay, maxIdleDelay))
            {
                StartCoroutine(NPCIdleForSec(RandomNumGen(minIdleTime, maxIdleTime)));
                movementTimer = 0;
            }
            else if (isIdle)
            {
                movementTimer = 0;
            }
        }
    }
    protected void NPCDirection(){
        if (currentPoint != null){
            if (transform.position.x > currentPoint.position.x + 1){
                direction = -1;
            }
            else if (transform.position.x < currentPoint.position.x - 1){
                direction = 1;
            }
        }
    }
    public abstract void Idle(Boolean isIdle);
    IEnumerator NPCIdleForSec(float idleTime){
        if (rb.velocity.x != 0){
            Idle(true);
            yield return new WaitForSeconds(idleTime);
            Idle(false);
        }
    }
    //========================================================================================================================================================
    public abstract void ChangeStatus(Status status);
    protected void flip(){
        Vector3 scale = transform.localScale;
        if (direction == -1 || direction == 1){
            scale.x = direction;
        }
        transform.localScale = scale;
    }
}
