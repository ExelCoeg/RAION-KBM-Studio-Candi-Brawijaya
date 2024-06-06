
using UnityEngine;
using TMPro;
public class BuildingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buildingInfo;
    Building building;
    private void Awake() {
        building = GetComponentInParent<Building>();
    }
    // Update is called once per frame
    void Update()
    {
        buildingInfo.text = "Health: " + building.currentHealth + " / " + building.maxHealth + "\n" +
                            "Level : " + building.currentLevel + "\n" +
                            "Upgrade Cost: " + building.upgradeCost + "\n" +
                            "Recover Cost: " + building.recoverBuildingCost;
    }

}
