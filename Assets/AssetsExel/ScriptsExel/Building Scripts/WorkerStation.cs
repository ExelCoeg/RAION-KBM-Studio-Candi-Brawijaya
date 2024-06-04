
using UnityEngine;

public class WorkerStation : MonoBehaviour
{
    public int offset = 1;
    [SerializeField] GameObject workerStationUI;

    [SerializeField] GameObject EIcon;
    void Update()
    {
        
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y - offset), 3f, LayerMask.GetMask("Player"));
        if(player !=null){
            EIcon.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F)){
                workerStationUI.gameObject.SetActive(true);
                workerStationUI.GetComponent<WorkerStationUI>().closeInput = false;
            }
        }

        else if(player== null){
            EIcon.SetActive(false);
            workerStationUI.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - offset), 3f);
    }
}
