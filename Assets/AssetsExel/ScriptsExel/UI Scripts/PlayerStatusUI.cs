using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider energySlider;
    [SerializeField] Slider lifeEssenceSlider;
    
    void Update()
    {
        hpSlider.value = (float) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth /GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>()._maxHealth;
        energySlider.value =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().energy / GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().maxEnergy;
        lifeEssenceSlider.value = (float) GameObject.FindGameObjectWithTag("Player").GetComponent<Keris>().lifeEssence / 100;
    }

}
