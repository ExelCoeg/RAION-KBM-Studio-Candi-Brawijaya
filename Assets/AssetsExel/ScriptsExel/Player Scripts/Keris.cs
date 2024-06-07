
using UnityEngine.UI;
using UnityEngine;

public class Keris : MonoBehaviour
{
    [Header("Life Essence")]
    public int lifeEssence = 0;

    [Header("Petir Attributes")]
    private bool petirAvailable = true;
    [SerializeField] int petirCost = 10;
    private float petirCooldown;
    [SerializeField] float petirCooldownMax = 5f;
    public float petirOffset = 2f;
    [Header("Expand Attributes")]
    [SerializeField] float holdTime = 0f;

    [SerializeField] float requiredHoldTime = 3f;

    [SerializeField] Vector3 mousePos;

    [SerializeField] GameObject petir;
    [SerializeField] Slider expandBar;
    

    // Start is called before the first frame update
    void Start()
    {
        ResetPetirCooldown();
    }
   

    // Update is called once per frame
    void Update()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        // print("B: " + (transform.position.x <= TerritoryManager.instance.pointBx));
        // print("A: " + (transform.position.x >= TerritoryManager.instance.pointAx));
        petirCooldown -= Time.deltaTime;
        petirAvailable = lifeEssence >= petirCost && petirCooldown <= 0;

        if(Input.GetKeyDown(KeyCode.Space) && petirAvailable){
            // panggil fungsi petir
            Petir(new Vector3(mousePos.x + petirOffset, 0.5f));
            ResetPetirCooldown();
        }
        Expand();
    }

    public void Expand(){
    
        
        if(Input.GetKey(KeyCode.F)){
            // expand muncul bar untuk expand (WORLD SPACE UI)
                    
            expandBar.gameObject.SetActive(true);

            holdTime += Time.deltaTime;
            expandBar.value = holdTime/requiredHoldTime;

            if(holdTime >= requiredHoldTime){
                TerritoryManager.instance.territoryPoints.pointA.gameObject.GetComponent<ExpandTerritory>().Expand();
                expandBar.gameObject.SetActive(false);
            }
        }
        else{
            holdTime = 0;
            expandBar.gameObject.SetActive(false);
        }
    }
    
    public void Petir(Vector3 pos){ 
        GameObject petirClone = Instantiate(petir, pos, Quaternion.identity);
        
    }
    public void ResetPetirCooldown(){
        petirCooldown = petirCooldownMax;
    }


}

