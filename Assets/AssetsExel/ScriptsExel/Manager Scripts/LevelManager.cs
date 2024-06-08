using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int archerLevel = 1;
    public int knightLevel = 1;
    public int villagerLevel = 1;

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
        archerLevelText.text = archerLevel.ToString();
        knightLevelText.text = knightLevel.ToString();
        villagerLevelText.text = villagerLevel.ToString();
    }
    
}
