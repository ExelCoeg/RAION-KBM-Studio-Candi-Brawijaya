using UnityEngine;
public class NPCHealth : MonoBehaviour, IDamagable{
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
            print("Player dead");
            GetComponent<NPC>().ChangeStatus(Status.Vagrant);
        }
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        print(this.name + " taking damage");
        DamageVisualied();
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