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
            print(gameObject.tag);
            if(gameObject.tag.Equals("Gelapari") || gameObject.tag == "Gelapari"){
                print("Gelapari dead");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().lifeEssence += 50;
                GetComponent<Enemy>().Destroy();
            }
            else if(gameObject.tag.Equals("Widara") || gameObject.tag == "Widara"){
                print("Widara dead");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().lifeEssence += 25;
                GetComponent<Enemy>().Destroy();
            }
            else if(gameObject.tag.Equals("Lelumut") || gameObject.tag == "Lemulut"){
                print("Lemulut dead");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Keris>().lifeEssence += 15;
                GetComponent<Enemy>().Destroy();
            }
        }
    }
    public void heal(){
        currentHealth = maxHealth;
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