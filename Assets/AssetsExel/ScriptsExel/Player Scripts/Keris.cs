
using UnityEngine.UI;
using UnityEngine;

public class Keris : MonoBehaviour
{
    [Header("Life Essence")]
    public int lifeEssence = 0;
    [SerializeField] int maxLifeEssence = 50;

    [Header("Petir Attributes")]
    private bool petirAvailable = true;
    [SerializeField] int petirCost = 20;
    private float petirCooldown;
    public float petirChargeTime;
    float petirChargeTimer;
    [SerializeField] float petirCooldownMax = 5f;
    public float petirOffsetX = 2f;
    public float petirOffsetY = 2f;
    [Header("Expand Attributes")]
    [SerializeField] float holdTime = 0f;

    [SerializeField] float requiredHoldTime = 3f;
    [SerializeField] int expandCost = 100;

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

        if(Input.GetKeyDown(KeyCode.Space) && petirAvailable && lifeEssence >=  petirCost){
            // panggil fungsi petir
            GetComponent<Animator>().Play("player_strike");
            ResetPetirCooldown();
        }
        Expand();
    }

    public void Expand(){
    
        
        if(Input.GetKey(KeyCode.F) && (TerritoryManager.instance.onPointA || TerritoryManager.instance.onPointB) ){
            // expand muncul bar untuk expand (WORLD SPACE UI)
                    

            
            GetComponent<Animator>().Play("player_expand");
            if(lifeEssence <= expandCost){
                expandBar.gameObject.SetActive(false);
                GetComponent<Animator>().Play("player_idle");
                return;
            }
            else{
                holdTime += Time.deltaTime;
                expandBar.value = holdTime/requiredHoldTime;
                expandBar.gameObject.SetActive(true);
            }
            if(holdTime >= requiredHoldTime){
                if(lifeEssence >= expandCost){
                    TerritoryManager.instance.territoryPoints.pointA.gameObject.GetComponent<ExpandTerritory>().Expand();
                    reduceLifeEssence(25);
                }
                else{
                    expandBar.gameObject.SetActive(false);
                    GetComponent<Animator>().Play("player_idle");
                    return;
                }
            }
        }
        else{
            holdTime = 0;
            
            GetComponent<Animator>().SetBool("isCasting",false);
            expandBar.gameObject.SetActive(false);
        }
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
    public float getMousePosX(){
        return mousePos.x;
    }
    public Slider getExpandBar(){
        return expandBar;
    }
}

