using UnityEngine;
public class EnemyHealth : MonoBehaviour, IDamagable{
    [SerializeField] int maxHealth = 100;
    public int health {get;set;}
    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        if(health <= 0){
            print("Player dead");
            this.GetComponent<Enemy>().Destroy();
        }
    }
    public void TakeDamage(int damage){
        maxHealth -= damage;
        print(this.name + " taking damage");
    }

}