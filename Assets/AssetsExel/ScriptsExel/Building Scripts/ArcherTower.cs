
using Unity.IO.Archive;
using UnityEngine;

public class ArcherTower : Building
{
    
    bool filled = false;
    
    public GameObject archer;

    public override void Update()
    {
        base.Update();
        FillTowerWithArcher();
        if (archer != null)
        {
            print("archer available");
            archer.GetComponent<ArcherScript>().InTower();
        }
    }
    
    public void FillTowerWithArcher(){
        if(currentHealth == maxHealth && !filled){
            archer = GameObject.FindGameObjectWithTag("Archer");
            if(archer != null){  
                Invoke("SetUpArcher",1);
                filled = true;
            }
            else{
                print("No archer available");
            }
        }
        
    }
    public void SetUpArcher(){
        archer.tag = "Untagged";
        archer.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        archer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        archer.GetComponent<ArcherScript>().SetPoint(transform);
    }
    public override void ChangeBuildingSprite(){
        if(currentHealth <= 0){
            GetComponent<SpriteRenderer>().sprite = spriteStages[2];
            GetComponent<BoxCollider2D>().enabled = false;

            if(archer != null ){
                archer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                archer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                archer.tag = "Archer";
                archer = null;

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


    
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - 2f), 3f);
    }

}
