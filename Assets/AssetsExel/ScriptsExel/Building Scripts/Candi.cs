
using UnityEngine;

public class Candi : MonoBehaviour, IDamagable
{
    public int _maxHealth {get;set;}
    public int _health {get;set;}
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth <= 0){
            //animasi
            gameOverUI.SetActive(true);
        }
    }
}
