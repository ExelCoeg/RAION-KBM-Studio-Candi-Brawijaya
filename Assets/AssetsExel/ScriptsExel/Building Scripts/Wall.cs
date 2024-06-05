
using UnityEngine;

public class Wall : Building
{

    void Update()
    {
        ChangeBuildingSprite();
    }
    public override void ChangeBuildingSprite(){
        if(currentHealth <= 0){
            GetComponent<SpriteRenderer>().sprite = spriteStages[3];
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(currentHealth <= 40){
            GetComponent<SpriteRenderer>().sprite = spriteStages[2];
        }
        else if(currentHealth <= 60){
            GetComponent<SpriteRenderer>().sprite = spriteStages[1];
        }
        else if(currentHealth <= 100){
            GetComponent<SpriteRenderer>().sprite = spriteStages[0];
        }
    }

    public override void Upgrade(){
        currentLevel++;
        if(currentLevel.Equals(1)){
            maxHealth = 200;
            currentHealth = maxHealth;
        }
        else if(currentLevel.Equals(2)){
            maxHealth = 300;
            currentHealth = maxHealth;
        }
        else if(currentLevel.Equals(3)){
            maxHealth = 400;
            currentHealth = maxHealth;
        }
    }
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - 2f), 3f);
    }
}
