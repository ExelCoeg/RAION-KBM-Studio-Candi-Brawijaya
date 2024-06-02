using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Enemy : MonoBehaviour
{
    public EnemyStatus enemyStatus;
    protected EnemyManager enemyManager;
    [SerializeField] public Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    [Header("Position")]
    protected PointManager pointManager;
    [SerializeField] protected Transform currentPoint;
    [SerializeField] protected EnemyPoints enemyPoints;
    [Header("Detection")]
    [SerializeField] protected LayerMask damageAbleLayer;
    [SerializeField] protected Collider2D objectDetected;
    [SerializeField] protected Collider2D objectInRange;
    [SerializeField] public float rangeDetection;
    [SerializeField] public float rangeAttack;

    [Header("Condition")]
    [SerializeField] protected bool damageAbleDetected;//npc/player
    [SerializeField] protected bool damageAbleInRange;//npc/player dalam jangkauan
    [SerializeField] protected bool isIdle;//pada NPC kondisi Idle bisa berbeda tergantung job masing masing
    [SerializeField] protected bool isNight;//waktunya berburu
    [Header("Atribut")]
    protected float movementTimer;//timer untuk Idle
    protected float attackTimer;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float movSpeed;//MovSpeed saat ini
    [SerializeField] protected float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected int direction;//Target
    [Header("IdleSet")]
    [SerializeField] protected float minIdleDelay;//nilai minimal jeda Sebelum Idle
    [SerializeField] protected float maxIdleDelay;//nilai maksimal jeda Sebelum Idle
    [SerializeField] protected float minIdleTime;//waktu mininal NPC Idle
    [SerializeField] protected float maxIdleTime;//waktu maksimal NPC Idle

    //========================================================================================================================================================
    public void ChangePoint()
    {
        if (currentPoint == enemyPoints.pointA && math.abs(currentPoint.position.x - rb.position.x) <= 0.5)
        {
            SetPoint(enemyPoints.pointB);
        }
        else if (currentPoint == enemyPoints.pointB && math.abs(currentPoint.position.x - rb.position.x) <= 0.5)
        {
            SetPoint(enemyPoints.pointA);
        }
    }
    public EnemyPoints SetPlace(EnemyPointNames left, EnemyPointNames right)
    {
        if (pointManager.GetEnemyPoints(left).enemyCount <= pointManager.GetEnemyPoints(right).enemyCount)
        {
            return pointManager.GetEnemyPoints(left);// nanti rencananya ada pembagian left sama rightnya
        }
        else
        {
            return pointManager.GetEnemyPoints(right);
        }
    }
    public EnemyPoints SetPlace(EnemyPointNames enemyPointsint)
    {
        return pointManager.GetEnemyPoints(enemyPointsint);
    }
    public void SetPoint(Transform transform)
    {
        currentPoint = transform;//current point adalah poin dari tujuan NPC
    }
    public void SetIdle(float[] idleSet)
    {
        minIdleDelay = idleSet[0];
        maxIdleDelay = idleSet[1];
        minIdleTime = idleSet[2];
        maxIdleTime = idleSet[3];
    }
    //========================================================================================================================================================
    public abstract void EnemyAttack();

    public virtual void Attack()
    {
        if(objectInRange != null){
            IDamagable damagable = objectInRange.GetComponent<IDamagable>();
            if (damagable != null)
            {
                objectInRange.GetComponent<IDamagable>().TakeDamage(20);
            }
        }
    }
    //========================================================================================================================================================
    protected float RandomNumGen(float min, float max) { return UnityEngine.Random.Range(min, max); }
    //========================================================================================================================================================
    protected void EnemyMovement()
    {
        rb.velocity = new Vector2(movSpeed * direction, rb.velocity.y);
    }
    protected void RandomIdle()
    {
        if (enemyPoints.pointA.position.x < transform.position.x && enemyPoints.pointB.position.x > transform.position.x)
        {
            movementTimer += Time.deltaTime;
            if (movementTimer >= RandomNumGen(minIdleDelay, maxIdleDelay))
            {
                StartCoroutine(EnemyIdleForSec(RandomNumGen(minIdleTime, maxIdleTime)));
                movementTimer = 0;
            }
            else if (isIdle)
            {
                movementTimer = 0;
            }
        }
    }
    protected void EnemyDirection()
    {
        if (currentPoint != null)
        {
            if (transform.position.x > currentPoint.position.x + 1)
            {
                direction = -1;
            }
            else if (transform.position.x < currentPoint.position.x - 1)
            {
                direction = 1;
            }
        }
    }

    public abstract void Idle(Boolean isIdle);

    IEnumerator EnemyIdleForSec(float idleTime)
    {
        if (rb.velocity.x != 0)
        {
            Idle(true);
            yield return new WaitForSeconds(idleTime);
            Idle(false);
        }
    }
    //========================================================================================================================================================
    public virtual void Destroy()
    {
        Destroy(this);
    }
    //========================================================================================================================================================
    protected abstract void CheckSurrounding();
    //========================================================================================================================================================

    protected void flip()
    {
        Vector3 scale = transform.localScale;
        if (direction == -1 || direction == 1)
        {
            scale.x = direction;
        }
        transform.localScale = scale;
    }
}
