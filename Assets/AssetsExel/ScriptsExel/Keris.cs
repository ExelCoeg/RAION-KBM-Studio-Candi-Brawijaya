
using Unity.VisualScripting;
using UnityEngine;

public class Keris : MonoBehaviour
{
    [SerializeField] GameObject petir;
    private int lifeEssence = 0;
    private bool petirAvailable = true;
    private float petirCooldown;
    [SerializeField] float petirCooldownMax = 5f;
    public float petirOffset = 2f;
    [SerializeField] Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        ResetPetirCooldown();
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        petirCooldown -= Time.deltaTime;
        // petirAvailable = lifeEssence >= 10 && petirCooldown <= 0;
        petirAvailable = petirCooldown <= 0;
        if(Input.GetKeyDown(KeyCode.Space) && petirAvailable){
            // panggil fungsi petir
            Petir(new Vector3(mousePos.x + petirOffset, 0.5f));
            ResetPetirCooldown();
        }
    }

    public void Expand(){
        // parameter : territory yang akan diperluas
        print("Keris expanding territory");
    }

    public void Petir(Vector3 pos){ 
        print("Keris summoning thunderstorm");
        GameObject petirClone = Instantiate(petir, pos, Quaternion.identity);
        
    }
    public void ResetPetirCooldown(){
        petirCooldown = petirCooldownMax;
    }

    public void AddLifeEssence(int amount){
        lifeEssence += amount;
    }
}

