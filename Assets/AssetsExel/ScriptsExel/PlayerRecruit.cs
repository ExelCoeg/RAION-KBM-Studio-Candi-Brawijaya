
using UnityEngine;

public class PlayerRecruit : MonoBehaviour
{
    public LayerMask npcLayer;
    public GameObject selectedNPC;
    private void Update() {
        Collider2D npc = Physics2D.OverlapCircle(transform.position, 1, npcLayer);
        if(npc != null){
            selectedNPC = npc.gameObject;
        }
        
        
    }

}
