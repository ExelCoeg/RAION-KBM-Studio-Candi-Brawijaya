using UnityEngine;
public class EnemyHealth : MonoBehaviour, IDamagable{
    public int maxHealth = 100;
    public int health2;
    [SerializeField] public int health {get;set;}
    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        health2 = health;
        if(health <= 0){
            print("Player dead");
            this.GetComponent<Enemy>().Destroy();
        }
    }
    public void heal(){
        health = maxHealth;
    }
    public void TakeDamage(int damage){
        health -= damage;
        print(this.name + " taking damage");
    }

}