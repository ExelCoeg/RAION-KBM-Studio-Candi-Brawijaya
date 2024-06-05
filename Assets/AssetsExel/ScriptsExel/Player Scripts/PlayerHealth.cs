using UnityEngine;
public class PlayerHealth : MonoBehaviour, IDamagable{
    [SerializeField] int maxHealth = 100;
    public int _health {get;set;}
    public int _maxHealth {get;set;}

    public int currentHealth;
    private void Start() {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
    }
    private void Update() {
        if(currentHealth <= 0){
            print("Player dead");
        }
    }
    public void TakeDamage(int damage){
        print("Player taking damage");
    }

}