using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int archerLevel = 1;
    public int knightLevel = 1;
    public int villagerLevel = 1;

    public int maxLevel = 15;
     [SerializeField] TextMeshProUGUI archerLevelText;
     [SerializeField] TextMeshProUGUI knightLevelText;
     [SerializeField] TextMeshProUGUI villagerLevelText;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Update() {
        if(archerLevel >= maxLevel){
            archerLevel = maxLevel;
            archerLevelText.text = "MAX";
        }
        else{
            archerLevelText.text = archerLevel.ToString();
        }
        if(knightLevel >= maxLevel){
            knightLevel = maxLevel;
            knightLevelText.text = "MAX";
        }
        else{
            knightLevelText.text = knightLevel.ToString();
        
        }
        if(villagerLevel >= maxLevel){
            villagerLevel = maxLevel;
            villagerLevelText.text = "MAX";
        }
        else{

            villagerLevelText.text = villagerLevel.ToString();
        }
    }
    
}
