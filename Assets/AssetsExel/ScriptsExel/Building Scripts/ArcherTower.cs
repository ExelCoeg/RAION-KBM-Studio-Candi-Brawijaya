using UnityEngine;
public class ArcherTower : Building
{
    
    bool filled = false;

    GameObject archer;

    void Update()
    {
        FillTowerWithArcher();
    }

    public void FillTowerWithArcher(){
        if(currentHealth == maxHealth && !filled){
            if(NPCManager.instance.archerCount > 0){
                archer = GameObject.FindGameObjectWithTag("Archer");
                archer.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                archer.tag = "Untagged";
                archer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                filled = true;
            }
            else{
                print("No archer available");
            }
        }
    }
    public override void ChangeBuildingSprite(){
        if(currentHealth <= 0){
            GetComponent<SpriteRenderer>().sprite = spriteStages[2];
            GetComponent<BoxCollider2D>().enabled = false;

            if(archer != null ){
                archer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                archer.tag = "Archer";
            }
            filled = false;

        }
        else if(currentHealth <= 40){
            GetComponent<SpriteRenderer>().sprite = spriteStages[1];
        }
       
        else if(currentHealth <= 100){
            GetComponent<SpriteRenderer>().sprite = spriteStages[0];
        }
    }


    public override void Upgrade()
    {
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount -= upgradeCosts[currentLevel-1];
    }
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - 2f), 3f);
    }

}
