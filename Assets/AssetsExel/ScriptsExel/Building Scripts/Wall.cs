
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int testHealth;
    public int health {get; set;}
    public Sprite[] wallStage;
    
    [SerializeField] GameObject EIcon;
    bool built = false;
    public void Start()
    {
        health = 0;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
  
    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y -2f), 3f, LayerMask.GetMask("Player"));
        if(!built){
            if(player != null){
                EIcon.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F)){
                    health = maxHealth;
                    built = true;
                    GetComponent<BoxCollider2D>().enabled = true;
                    EIcon.SetActive(false);
                }
            }
            else if(player == null){
                EIcon.SetActive(false);
            }
        }
        if(health <= 0){
            GetComponent<SpriteRenderer>().sprite = wallStage[3];
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(health <= 40){
            GetComponent<SpriteRenderer>().sprite = wallStage[2];
        }
        else if(health <= 60){
            GetComponent<SpriteRenderer>().sprite = wallStage[1];
        }
        else if(health <= 100){
            GetComponent<SpriteRenderer>().sprite = wallStage[0];
        }
    
    }
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - 2f), 3f);
    }
}
