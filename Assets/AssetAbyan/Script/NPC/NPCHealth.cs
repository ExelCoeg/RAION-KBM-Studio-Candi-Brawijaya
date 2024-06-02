using UnityEngine;
public class NPCHealth : MonoBehaviour, IDamagable{
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
        maxHealth -= damage;
        print(this.name + " taking damage");
    }

}