
using UnityEngine;

public class WorkerStation : MonoBehaviour
{
    public int offset = 1;
    [SerializeField] GameObject workerStationUI;
    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y - offset), 3f, LayerMask.GetMask("Player"));
        if(player !=null){
            workerStationUI.gameObject.SetActive(true);
        }
        else{
            workerStationUI.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y - offset), 3f);
    }
}
