using UnityEngine;
public class EnemyHealth : MonoBehaviour, IDamagable{

    public int _maxHealth {get;set;}
    public int _health {get;set;}
    public int maxHealth = 100;
    public int currentHealth;
    private void Start() {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
    }
    private void Update() {
        _maxHealth = maxHealth;
        _health = currentHealth;
        if(currentHealth <= 0){
            if(gameObject.tag == "Gelapari"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().addLifeEssence(3);
            }
            if(gameObject.tag == "Widara"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().addLifeEssence(2);
            }
            if(gameObject.tag == "Lelumut"){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().addLifeEssence(1);
            }
            GetComponent<Enemy>().Destroy();
        }
    }
    public void heal(){
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        print(this.name + " taking damage");
    }

}