
using TMPro;
using UnityEngine;

public class WorkerStationUI : MonoBehaviour
{
    public TextMeshProUGUI archerAttackSpeedText;
    public TextMeshProUGUI archerDamageText;
    public TextMeshProUGUI knightAttackSpeedText;
    public TextMeshProUGUI knightDamageText;
    public TextMeshProUGUI villagerMiningTimeToCoinText;

    private void Update() {
        archerAttackSpeedText.text = "ATKSPD " + NPCManager.instance.archerAttackSpeed.ToString();
        archerDamageText.text = "Damage " + NPCManager.instance.archerDamage.ToString();
        knightAttackSpeedText.text = "ATKSPD " + NPCManager.instance.knightAttackSpeed.ToString();
        knightDamageText.text = "Damage " + NPCManager.instance.knightDamage.ToString();
        villagerMiningTimeToCoinText.text = "Mine Time " +NPCManager.instance.villagerMiningTimeToCoin.ToString();
    }
    public void ExitUI(){
        gameObject.SetActive(false);
    }
    public void UpgradeKnight(){
        NPCManager.instance.knightAttackSpeed += 1.5f;
        NPCManager.instance.knightDamage += 1.5f;
       
    }
    public void UpgradeVillager(){

        NPCManager.instance.villagerMiningTimeToCoin -= 0.2f;
       
    }
    public void UpgradeArcher(){
        NPCManager.instance.archerAttackSpeed += 1.5f;
        NPCManager.instance.archerDamage += 1.5f;

    }
}
