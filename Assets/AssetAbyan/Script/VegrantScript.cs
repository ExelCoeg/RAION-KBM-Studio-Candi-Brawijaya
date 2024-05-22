using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VegrantScript : NPC
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] Boolean playerDetected;
    [SerializeField] float vegrantDetection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        npcManager = NPCManager.instance;
        pointManager = PointManager.instance;
        points = SetPlace(PointsNames.LeftVegrant,PointsNames.RightVegrant);
        SetPoint(points.pointA);
        SetIdle(npcManager.vegrantIdleSet);
        movSpeed = defaultMovSpeed;
        isIdle = false;
        points.NPCCount++;
        npcManager.vegrantCount++;
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetNPCAtribut();
        NPCDirection();
        ChangePoint();
        flip();
        CheckEnemyInRange();
    }
    private void FixedUpdate() {
        NPCMovement();
        RandomIdle();
    }
    private void SetNPCAtribut(){
        SetIdle(npcManager.vegrantIdleSet);
        vegrantDetection = npcManager.vegrantDetection;
    }
    private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position, vegrantDetection, playerLayer);
        Transform temp = null;

        if (objectInRange != null && objectInRange.tag == "Player" && !playerDetected){
            temp = currentPoint;
            SetPoint(objectInRange.gameObject.transform);
            playerDetected = true;
        }else if(playerDetected){
            SetPoint(temp);
            playerDetected = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, vegrantDetection);
    }
    private void OnDestroy() {
        npcManager.vegrantCount--;
    }
}
