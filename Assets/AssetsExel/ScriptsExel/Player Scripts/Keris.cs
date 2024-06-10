
using UnityEngine.UI;
using UnityEngine;

public class Keris : MonoBehaviour
{
    [Header("Life Essence")]
    public int lifeEssence = 0;
    [SerializeField] int maxLifeEssence = 50;

    [Header("Petir Attributes")]
    private bool petirAvailable = true;
    [SerializeField] int petirCost = 5;
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

        if(lifeEssence > maxLifeEssence){
            lifeEssence = maxLifeEssence;
        }

        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
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
    
        
        if(Input.GetKey(KeyCode.F) && (TerritoryManager.instance.onPointA || TerritoryManager.instance.onPointB)){
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
        reduceLifeEssence(25);
    }
    
    public void Petir(Vector3 pos){ 
        GameObject petirClone = Instantiate(petir, pos, Quaternion.identity);
        reduceLifeEssence(petirCost);
    }
    public void addLifeEssence(int amount){
        lifeEssence += amount;
    }
    public void reduceLifeEssence(int amount){
        lifeEssence -= amount;
    }
    public void ResetPetirCooldown(){
        petirCooldown = petirCooldownMax;
    }


}

