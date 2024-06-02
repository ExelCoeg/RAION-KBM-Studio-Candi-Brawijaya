using System;
using System.Collections;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WidaraScript : Enemy
{

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        enemyManager = EnemyManager.instance;
        pointManager = PointManager.instance;
        enemyPoints = SetPlace(EnemyPointNames.Right, EnemyPointNames.Left);
        SetPoint(enemyPoints.pointA);
        SetIdle(enemyManager.widaraIdleSet);
        movSpeed = enemyManager.widaraMovSpeed;
        attackSpeed = enemyManager.widaraAttackSpeed;
        damage = enemyManager.widaraDamage;

        enemyPoints.enemyCount++;
        enemyManager.widaraCount++;

        isIdle = false;
        damageAbleDetected = false;
        damageAbleInRange = false;

        enemyStatus = EnemyStatus.Widara;
        enemyHealth = this.GetComponent<EnemyHealth>();
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
        Flying();
    }
    //========================================================================================================================================================LookAt(Vector3 pos) {
    private void SetEnemyAtribut(){
        SetIdle(enemyManager.widaraIdleSet);
        rangeDetection = enemyManager.widaraDetection;
        rangeAttack = enemyManager.widaraRangeAttack;

        if (enemyHealth != null) {}
        {
            enemyHealth.maxHealth = (int)enemyManager.widaraHealth;   
        }
    }
    //========================================================================================================================================================
    
    protected void EnemyMovement(){
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);
    }
    protected void Flying(){
        transform.position = new Vector3(transform.position.x,7,transform.position.z);
    }
    
    public override void Idle(Boolean isIdle) {
        if (isIdle){
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = enemyManager.widaraMovSpeed;
        }
    }
    //========================================================================================================================================================
    public override void EnemyAttack(){
        attackTimer += Time.deltaTime;
        if(damageAbleInRange){
            if (attackTimer > enemyManager.widaraAttackSpeed)
            {
                animator.Play("WidaraAttack");
                attackTimer = 0;
            }
        }else if(attackTimer > enemyManager.widaraAttackSpeed){
            attackTimer = enemyManager.widaraAttackSpeed;
        }
    }
    public override void Attack()
    {
        if(objectInRange != null){
            IDamagable damagable = objectInRange.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage((int)enemyManager.widaraDamage);
            }
        }
    }
    //========================================================================================================================================================
    protected override void CheckSurrounding(){
        objectDetected = Physics2D.OverlapCircle(transform.position, rangeDetection, damageAbleLayer);
        objectInRange = Physics2D.OverlapCircle(attackPoint.position, rangeAttack, damageAbleLayer);

        if (objectDetected != null && enemyManager.widaraDamageAble.Contains(objectDetected.tag)){
            SetPoint(objectDetected.gameObject.transform);
            damageAbleDetected = true;
        } else if(damageAbleDetected){
            SetPoint(enemyPoints.pointA);
            damageAbleDetected =false;
        }
        if (objectInRange != null && enemyManager.widaraDamageAble.Contains(objectInRange.tag)){
            Idle(true);
            damageAbleInRange = true;
        } else if(damageAbleInRange){
            Idle(false);
            damageAbleInRange =false;
        }
    }
    //========================================================================================================================================================
    public override void Destroy(){
        enemyManager.widaraCount--;
        enemyPoints.enemyCount--;
        base.Destroy();
    }
    //========================================================================================================================================================
    
    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.DrawWireSphere(attackPoint.position, rangeAttack);
    }
    //========================================================================================================================================================
    private void setAnimationParameter(){
        animator.SetFloat("movSpeed",Mathf.Abs(movSpeed));
    }
}