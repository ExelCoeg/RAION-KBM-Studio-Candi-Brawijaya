
using UnityEngine;

public class Candi : MonoBehaviour, IDamagable
{
    public int _maxHealth {get;set;}
    public int _health {get;set;}
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth <= 0){
            //animasi
        }
    }
}
