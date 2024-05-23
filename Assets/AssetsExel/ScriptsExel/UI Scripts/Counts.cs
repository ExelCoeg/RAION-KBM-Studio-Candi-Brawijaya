
using UnityEngine;
using TMPro;
public class Counts : MonoBehaviour
{
    public TextMeshProUGUI knightCountText;
    public TextMeshProUGUI archerCountText;
    public TextMeshProUGUI villagerCountText;
    
    void Update()
    {
        knightCountText.text = NPCManager.instance.knightCount.ToString();
        archerCountText.text = NPCManager.instance.archerCount.ToString();
        villagerCountText.text =  NPCManager.instance.villagerCount.ToString();
    }
}
