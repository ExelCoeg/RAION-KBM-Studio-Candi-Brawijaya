using UnityEngine;
public class PlayerHealth : MonoBehaviour, IDamagable{
    [SerializeField] int maxHealth = 100;
    public int _health {get;set;}
    public int _maxHealth {get;set;}

    public int currentHealth;

    [SerializeField] GameObject gameOverUI;
    private void Start() {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
    }
    private void Update() {
        if(currentHealth <= 0){
            gameOverUI.SetActive(true);
        }
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }

}