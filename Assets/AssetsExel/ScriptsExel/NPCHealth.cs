using UnityEngine;

public class NPCHealth : MonoBehaviour, IDamagable {
    public int health {get;set;}
    [SerializeField] int maxHealth;
    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        if(health<= 0){
            Destroy(gameObject);
        }
    }
}