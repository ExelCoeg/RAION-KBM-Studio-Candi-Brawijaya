using UnityEngine;
public class NPCHealth : MonoBehaviour, IDamagable{
    public int maxHealth = 100;
    public int health {get;set;}
    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        if(health <= 0){
            print("Player dead");
            this.GetComponent<NPC>().ChangeStatus(Status.Vagrant);
        }
    }
    public void TakeDamage(int damage){
        health -= damage;
        print(this.name + " taking damage");
    }

}