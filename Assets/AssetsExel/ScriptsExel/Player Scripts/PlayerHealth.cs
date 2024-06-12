using UnityEngine;
public class PlayerHealth : MonoBehaviour, IDamagable{
    [SerializeField] int maxHealth = 100;
    public int _health {get;set;}
    public int _maxHealth {get;set;}

    public int currentHealth;

    public SpriteRenderer[] spriteRenderers;

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
        DamageVisualied();
    }
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