using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GelaspariScript : Enemy
{
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        enemyManager = EnemyManager.instance;
        pointManager = PointManager.instance;
        enemyPoints = SetPlace(EnemyPointNames.Right, EnemyPointNames.Left);//nanti ini bakala dipake spawner 
        SetPoint(enemyPoints.pointA);
        SetIdle(enemyManager.gelapariIdleSet);
        movSpeed = enemyManager.gelapariMovSpeed;
        attackSpeed = enemyManager.gelapariAttackSpeed;
        damage = enemyManager.gelapariDamage;

        enemyPoints.enemyCount++;
        enemyManager.gelapariCount++;

        isIdle = false;
        damageAbleDetected = false;
        damageAbleInRange = false;

        enemyStatus = EnemyStatus.Gelapari;
        enemyHealth = this.GetComponent<EnemyHealth>();
        enemyHealth.maxHealth = (int)enemyManager.gelapariHealth; 
        enemyHealth.heal();

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
        SetIdle(enemyManager.gelapariIdleSet);
        rangeDetection = enemyManager.gelapariDetection;
        rangeAttack = enemyManager.gelapariRangeAttack;

        if (enemyHealth != null) {}
        {
            enemyHealth.maxHealth = (int)enemyManager.gelapariHealth;   
        }
    }
    //========================================================================================================================================================
    public override void Idle(Boolean isIdle) {
        if (isIdle){
            Debug.Log("aoidfsujha;odsfkjjhna;osdihjf : " + isIdle);
            this.isIdle = true;
            movSpeed = 0;
        }
        else{
            this.isIdle = false;
            movSpeed = enemyManager.gelapariMovSpeed;
        }
    }
    //========================================================================================================================================================
    public override void EnemyAttack(){
        attackTimer += Time.deltaTime;
        if(damageAbleInRange){
            if (attackTimer > enemyManager.gelapariAttackSpeed)
            {
                animator.Play("GelapariAttack");
                attackTimer = 0;
            }
        }else if(attackTimer > enemyManager.gelapariAttackSpeed){
            attackTimer = enemyManager.gelapariAttackSpeed;
        }
    }
    public override void Attack()
    {
        if(objectInRange != null){
            IDamagable damagable = objectInRange.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage((int)enemyManager.gelapariDamage);
            }
        }
    }
    //======================================================================================================================================================
    protected override void CheckSurrounding(){
        objectDetected = Physics2D.OverlapCircle(transform.position, rangeDetection, damageAbleLayer);
        objectInRange = Physics2D.OverlapCircle(attackPoint.position, rangeAttack, damageAbleLayer);

        if (objectDetected != null && enemyManager.gelapariDamageAble.Contains(objectDetected.tag)){
            SetPoint(objectDetected.gameObject.transform);
            damageAbleDetected = true;
        } else if(damageAbleDetected){
            SetPoint(enemyPoints.pointA);
            damageAbleDetected =false;
        }
        if (objectInRange != null && enemyManager.gelapariDamageAble.Contains(objectInRange.tag)){
            Idle(true);
            damageAbleInRange = true;
        } else if(damageAbleInRange){
            Idle(false);
            damageAbleInRange =false;
        }
    }
    //========================================================================================================================================================
    public override void Destroy(){
        enemyManager.gelapariCount--;
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