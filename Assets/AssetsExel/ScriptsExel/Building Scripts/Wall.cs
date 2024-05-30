
using UnityEngine;

public class Wall : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int testHealth;
    public int health {get; set;}
    public Sprite[] wallStage;


    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    // private void Start() {
    //     health = maxHealth;
    // }
    void Update()
    {
        
        health = testHealth;
        if(health <= 0){
            GetComponent<SpriteRenderer>().sprite = wallStage[3];
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(health <= 20){
            GetComponent<SpriteRenderer>().sprite = wallStage[2];
        }
        else if(health <= 40){
            GetComponent<SpriteRenderer>().sprite = wallStage[1];
        }
        else if(health <= 60){
            GetComponent<SpriteRenderer>().sprite = wallStage[0];
        }
    }
}
