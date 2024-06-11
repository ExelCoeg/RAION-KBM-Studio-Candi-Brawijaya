using UnityEngine;
public abstract class Building : MonoBehaviour, IDamagable, IInteractable, IUpgradable{
    public int _maxHealth { get; set; }
    public int _health { get; set; }
    public int _level { get; set; } = 1;
    public int _maxLevel { get; set; }
    [Header("Building Attributes")]
    public int maxHealth;
    public int maxHealthIncrement; // penambahan maxHealth setiap kali upgrade
    public int maxLevel;
    public int currentHealth = 0;
    public int currentLevel = 1;
    [Header("Building Costs")]  
    public int buildingCost;
    public int upgradeCostMultiplier;
    public int upgradeCost = 1;
    public int recoverBuildingCost = 1;
    bool built;
    bool canBuild = false;
    [Header("Building Sprites & Icon")]
    public Sprite[] spriteStages;
    [SerializeField] GameObject FIcon;
    [SerializeField] GameObject buildingInfo;
    
    public virtual void Update() {
        // currentHealth = _health; // <- untuk testing
        _health = currentHealth;
        _level = currentLevel;
        upgradeCost = currentLevel * upgradeCostMultiplier;
        Interact();
        ChangeBuildingSprite();
        if(!canBuild && !built){
            if(transform.position.x >= TerritoryManager.instance.pointAx && transform.position.x <= TerritoryManager.instance.pointBx){
                canBuild = true;
            }
        }
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    public void Build(){
        if(Input.GetKeyDown(KeyCode.F)){
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount >= buildingCost){
                currentHealth = maxHealth;
                built = true;
                canBuild = false;
                GetComponent<BoxCollider2D>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount -= buildingCost;
            }
        }
    }
    public void Interact()
    {   
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y -2f), 3f, LayerMask.GetMask("Player"));
        if(player!=null && (canBuild || built)) {
            FIcon.SetActive(true);
            if(built) buildingInfo.SetActive(true);
            else buildingInfo.SetActive(false);
        }
        else {
            FIcon.SetActive(false);
            buildingInfo.SetActive(false);
        }
        if(canBuild && !built){
            if(player!= null){
                if(Input.GetKeyDown(KeyCode.F)) Build();
            }
        }
        else if(!canBuild && built){
            if(player!=null){
                if(Input.GetKeyDown(KeyCode.F)){
                    if(currentHealth < maxHealth) RecoverBuilding();
                    else if(currentHealth == maxHealth)Upgrade(upgradeCostMultiplier);
                }
            }
        }
    }
    
    public void RecoverBuilding()
    {
        if(currentHealth < maxHealth && GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount >= recoverBuildingCost){
            currentHealth = 20;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount -= recoverBuildingCost;
        }
    }
    
    public void Upgrade(int multiplier = 1)
    {
        if(currentLevel < maxLevel && GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount >= upgradeCost){
            currentLevel++;
            maxHealth += maxHealthIncrement;
            currentHealth = maxHealth;
            recoverBuildingCost += currentLevel % 3 == 0 ? 1 : 0 ;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount -= upgradeCost;
        }
    }
    
    public abstract void ChangeBuildingSprite();

}