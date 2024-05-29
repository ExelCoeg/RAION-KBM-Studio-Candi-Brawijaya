using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour,IDamagable
{
    [SerializeField] int maxHealth = 100;
    public int health;
    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        if(health <= 0){
            print("Player dead");
        }
    }
    public void TakeDamage(int damage){
        print("Player taking damage");

    }
}
