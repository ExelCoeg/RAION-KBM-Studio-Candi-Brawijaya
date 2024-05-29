using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LemulutScript : Enemy
{

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        enemyManager = EnemyManager.instance;
        pointManager = PointManager.instance;
        enemyPoints = SetPlace(EnemyPointNames.Right, EnemyPointNames.Left);
        SetPoint(enemyPoints.pointA);
        SetIdle(enemyManager.lemulutIdleSet);
        movSpeed = enemyManager.lemulutMovSpeed;
        attackSpeed = enemyManager.lemulutAttackSpeed;
        damage = enemyManager.lemulutDamage;

        enemyPoints.enemyCount++;
        enemyManager.lemulutCount++;

        isIdle = false;
        damageAbleDetected = false;
        damageAbleInRange = false;

        enemyStatus = EnemyStatus.Lemulut;
    }
    private void Update() {
        SetEnemyAtribut();
        EnemyDirection();
        ChangePoint();
        flip();
        CheckSurrounding();
        RandomIdle();
        setAnimationParameter();
        EnemyAttack();
    }
    private void FixedUpdate() {
        EnemyMovement();
    }
    //========================================================================================================================================================LookAt(Vector3 pos) {
    private void SetEnemyAtribut(){
        SetIdle(enemyManager.lemulutIdleSet);
        rangeDetection = enemyManager.lemulutDetection;
        rangeAttack = enemyManager.lemulutRangeAttack;
    }
    //========================================================================================================================================================
    public override void Idle(Boolean isIdle) {
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = enemyManager.lemulutMovSpeed;
        }
    }
    //========================================================================================================================================================
    public override void EnemyAttack(){
        attackTimer += Time.deltaTime;
        if(damageAbleInRange){
            if (attackTimer > enemyManager.lemulutAttackSpeed)
            {
                animator.Play("LemulutAttack");
                attackTimer = 0;
            }
        }else if(attackTimer > enemyManager.lemulutAttackSpeed){
            attackTimer = enemyManager.lemulutAttackSpeed;
        }
    }
    //========================================================================================================================================================
    protected override void CheckSurrounding(){
        objectDetected = Physics2D.OverlapCircle(transform.position, rangeDetection, damageAbleLayer);
        objectInRange = Physics2D.OverlapCircle(attackPoint.position, rangeAttack, damageAbleLayer);

        if (objectDetected != null && (objectDetected.tag == "NPC" || objectDetected.tag == "Player")){
            SetPoint(objectDetected.gameObject.transform);
            damageAbleDetected = true;
        } else if(damageAbleDetected){
            SetPoint(enemyPoints.pointA);
            damageAbleDetected =false;
        }
        if (objectInRange != null && (objectInRange.tag == "NPC" || objectInRange.tag == "Player")){
            SetPoint(objectInRange.gameObject.transform);
            damageAbleInRange = true;
        } else if(damageAbleDetected){
            SetPoint(enemyPoints.pointA);
            damageAbleInRange =false;
        }
    }
    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.DrawWireSphere(attackPoint.position, rangeAttack);
    }
    //========================================================================================================================================================
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed",Mathf.Abs(movSpeed));
    }
}