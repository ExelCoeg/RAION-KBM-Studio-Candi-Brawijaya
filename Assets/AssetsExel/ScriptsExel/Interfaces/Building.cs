using UnityEngine;

public abstract class Building : MonoBehaviour, IDamagable, IInteractable, IUpgradable{
    public int _maxHealth { get; set; }
    public int _health { get; set; }
    public int _level { get; set; }
    public int _maxLevel { get; set; }
    [Header("Building Attributes")]
    public int maxLevel;
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
        upgradeCosts = new int[maxLevel];
        currentHealth = 0 ;
    }
    private void Update() {
        Interact();
        ChangeBuildingSprite();
        // currentHealth = _health; // <- untuk testing
        _health = currentHealth;
        _level = currentLevel;
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
    }
    public void Build(){
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y -2f), 3f, LayerMask.GetMask("Player"));
        
        if(!built && canBuild){
            if(player != null){
                FIcon.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F)){
                    currentHealth = maxHealth;
                    built = true;
                    GetComponent<BoxCollider2D>().enabled = true;
                    FIcon.SetActive(false);
                }
            }
            else if(player == null){
                FIcon.SetActive(false);
            }
        }
    }
    public void Interact()
    {
         if(canBuild){
            if(!built){
                Build();
            }
            else if(built && currentHealth == maxHealth){
                Upgrade();
            }
            else if(built && currentHealth < maxHealth){
                RecoverBuilding();
            }
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