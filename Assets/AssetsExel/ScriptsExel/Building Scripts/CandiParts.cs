
using UnityEngine;

public class CandiParts : MonoBehaviour
{
    int currentHealth;
    [SerializeField] Sprite[] candiSprites;

    // Update is called once per frame
    void Update()
    {
        currentHealth = GetComponentInParent<Candi>().currentHealth;
        if(currentHealth <= 75){
            GetComponent<SpriteRenderer>().sprite = candiSprites[7];
        }
        else if(currentHealth <= 50){
            GetComponent<SpriteRenderer>().sprite = candiSprites[6];
        }
        else if(currentHealth <= 25){
            GetComponent<SpriteRenderer>().sprite = candiSprites[5];
        }
        else if(currentHealth <= 0){
            GetComponent<SpriteRenderer>().sprite = candiSprites[4];
            currentHealth = 0;
            //play animasi ancur then gameover
        }
       
    }
}
