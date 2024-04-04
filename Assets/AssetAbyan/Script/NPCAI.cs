
using UnityEngine;

enum Status
    {
        Poor,Village,Archer
    }
public class NPCAI : MonoBehaviour
{
    Points points;
    [SerializeField] private int movSpeed;
    private int direction = 1;
    private Rigidbody2D rb;
    private Transform currentPoint;//point that NPC go

    private void Awake() {
        ChangeStatus(Status.Poor);
        currentPoint = points.pointA;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        NPCMovement();
    }
    private void FixedUpdate() {
        rb.velocity = new Vector2 (movSpeed*direction,0);
    }
    private void ChangeStatus(Status status){
        points = PointManager.instance.getPoint(PointsNames.PoorLeft);
    }
    private void NPCMovement(){
        Debug.Log(currentPoint.position.x);
        if (currentPoint == points.pointA){
            if (transform.position.x > currentPoint.position.x){
                direction = -1;
            }else{
                direction = 1;
            }
        }
    }
}
