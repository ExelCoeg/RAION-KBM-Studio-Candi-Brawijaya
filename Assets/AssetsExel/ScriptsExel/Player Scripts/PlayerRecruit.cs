using UnityEngine;

public class PlayerRecruit : MonoBehaviour
{
    [SerializeField] LayerMask npcLayer;
    private GameObject selectedNPC;
    public GameObject chooseJobUI;
    
    private void Update() {
        Collider2D npc = Physics2D.OverlapCircle(transform.position, 1, npcLayer);
        print(npc != null && npc.gameObject.GetComponent<VagrantScript>());
        if(npc != null && npc.gameObject.GetComponent<VagrantScript>() && Input.GetKeyDown(KeyCode.E)){
            selectedNPC = npc.gameObject;
            chooseJobUI.SetActive(true);
        }
    }

    public GameObject getSelectedNPC(){
        return selectedNPC;
    }
}
