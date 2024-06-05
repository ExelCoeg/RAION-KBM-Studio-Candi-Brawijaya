using UnityEngine;

public abstract class Building : MonoBehaviour, IDamagable, IInteractable, IUpgradable{
    public int _maxHealth { get; set; }
    public int _health { get; set; }
    public int _level { get; set; }
    [Header("Building Attributes")]
    public int maxHealth;
    public int currentHealth;
    public int currentLevel;
    public int[] upgradeCosts;
    int recoverBuildingCost = 1;
    bool built;
    bool canBuild = true;
    [Header("Building Sprites & Icon")]
    public Sprite[] spriteStages;
    [SerializeField] GameObject FIcon;
    private void Start() {
        currentHealth = 0 ;
    }
    public virtual void Update() {
        // currentHealth = _health; // <- untuk testing
        _health = currentHealth;
        _level = currentLevel;
        Interact();
        ChangeBuildingSprite();
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    public void Build(){
        if(Input.GetKeyDown(KeyCode.F)){
            currentHealth = maxHealth;
            built = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void Interact()
    {
        if(canBuild){
            Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y -2f), 3f, LayerMask.GetMask("Player"));
            if(player!=null){
                FIcon.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F)){
                    if(!built) Build();
                    else{
                        if(currentHealth < maxHealth)RecoverBuilding();
                        else if(currentHealth == maxHealth)Upgrade();
                    }
                }
            }
            else FIcon.SetActive(false);
        }
    }
    
    public void RecoverBuilding()
    {
        recoverBuildingCost += currentLevel % 3 == 0 ? 1 : 0 ;
        if(currentHealth < maxHealth){
            currentHealth += 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().coinCount -= recoverBuildingCost;
        }
    }
    public abstract void Upgrade();
    public abstract void ChangeBuildingSprite();

}