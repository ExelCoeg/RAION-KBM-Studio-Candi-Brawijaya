
using UnityEngine;

public class Changer : MonoBehaviour
{
    [SerializeField] Status status;
    [SerializeField] Collider2D objectInRange;
    [SerializeField] float detection;
    [SerializeField] LayerMask NPCLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyInRange();
    }
     private void CheckEnemyInRange(){
        objectInRange = Physics2D.OverlapCircle(transform.position,detection,NPCLayer);

        if (objectInRange != null && objectInRange.tag == "NPC"){
            objectInRange.GetComponent<NPC>().ChangeStatus(status);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detection);
    }
}
