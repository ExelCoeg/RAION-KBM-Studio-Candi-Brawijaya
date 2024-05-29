using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GelapariScript : Enemy
{

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        
    }
    private void Update() {
        
    }
    private void FixedUpdate() {
        
    }
    //========================================================================================================================================================
    
    public override void Idle(Boolean isIdle) {

    }
    //========================================================================================================================================================
    public override void EnemyAttack(){

    }
    //========================================================================================================================================================
    protected override void CheckSurrounding(){

    }
    //
}