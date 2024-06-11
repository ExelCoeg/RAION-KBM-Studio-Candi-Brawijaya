
using UnityEngine;

public class WorkerStation : MonoBehaviour, IInteractable
{

    bool locked = true;
    public int offset = 1;
    [SerializeField] GameObject workerStationUI;
    
    [SerializeField] GameObject NPCCountsUI;
    [SerializeField] GameObject EIcon;

    void Update()
    {
       
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y - offset), 5f, LayerMask.GetMask("Player"));
        if(player !=null){
                EIcon.SetActive(true);
            if(!locked){
                if(Input.GetKeyDown(KeyCode.F)){
                    workerStationUI.gameObject.SetActive(true);
                    // NPCCountsUI.gameObject.SetActive(false);
                    workerStationUI.GetComponent<WorkerStationUI>().closeInput = false;
                }
            }
            if(locked){
                if(Input.GetKeyDown(KeyCode.F)){
                    EIcon.transform.position = new Vector2(EIcon.transform.position.x, EIcon.transform.position.y - 6);
                    gameObject.GetComponent<Animator>().Play("workerstation_muncul");
                    locked= false;
                }   
            }
        }

        else if(player== null){
            EIcon.SetActive(false);
            workerStationUI.gameObject.SetActive(false);
        }
        // if(!workerStationUI.activeSelf) NPCCountsUI.gameObject.SetActive(true); 
    }

    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - offset), 5f);
    }

    public void Interact()
    {
        
    }
}
