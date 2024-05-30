using UnityEngine;
public class PlayerHealth : MonoBehaviour, IDamagable{
    [SerializeField] int maxHealth = 100;
    public int health {get;set;}
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