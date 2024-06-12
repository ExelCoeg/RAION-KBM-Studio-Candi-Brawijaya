
using System;
using UnityEngine;

public class Candi : MonoBehaviour, IDamagable
{
    public int _maxHealth {get;set;}
    public int _health {get;set;}
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = maxHealth;
        currentHealth = _maxHealth;
        
    }
    private void Update() {
        // animator.SetFloat("Health", currentHealth);
        animator.SetFloat("Health", (((float)currentHealth/(float)maxHealth)*100f));
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        DamageVisualied();
        if(currentHealth <= 0){
            //animasi
            gameOverUI.SetActive(true);
        }
    }
    public SpriteRenderer[] spriteRenderers;
    public void DamageVisualied(){
        foreach(SpriteRenderer item in spriteRenderers){
            item.color = Color.red;
            Debug.Log("Kontol memek asu");
        }
        Invoke("backToWhite",0.1f);
    }
    public void backToWhite(){
        foreach(SpriteRenderer item in spriteRenderers){
            item.color = Color.white;
        }
    }
}
