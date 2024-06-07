
using UnityEngine;

public class TerritoryManager : MonoBehaviour
{

    public static TerritoryManager instance;
    public Points territoryPoints;
    public float pointAx,pointBx;
    public bool onPointA, onPointB;
    Vector2 raycastPointA,raycastPointB;
    [SerializeField] GameObject FKeyIconA;
    [SerializeField] GameObject FKeyIconB;
    

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this);
        }
        pointAx = territoryPoints.pointA.position.x;
        pointBx = territoryPoints.pointB.position.x;
    }
    private void Update() {
        raycastPointA = new Vector2(territoryPoints.pointA.position.x ,territoryPoints.pointA.position.y- 2f);
        raycastPointB = new Vector2(territoryPoints.pointB.position.x ,territoryPoints.pointB.position.y- 2f );

        Collider2D hitA =  Physics2D.OverlapCircle(territoryPoints.pointA.position,5f,LayerMask.GetMask("Player"));
        Collider2D hitB =  Physics2D.OverlapCircle(territoryPoints.pointB.position,5f,LayerMask.GetMask("Player"));

        if(hitA != null) 
        {
            onPointA = true;
            FKeyIconA.SetActive(true);
        }
        else{
            onPointA = false;       
            FKeyIconA.SetActive(false);
        } 

        if(hitB != null) {
            onPointB = true;
            FKeyIconB.SetActive(true);

        }
        else{
            onPointB = false;
            FKeyIconB.SetActive(false);
        } 
    }
    public GameObject GetBorderFromPointA(){
        RaycastHit2D hit = Physics2D.Raycast(raycastPointA, Vector2.left);
        if(hit.collider.gameObject.TryGetComponent<ExpandTerritory>(out ExpandTerritory expandTerritory)){
            return expandTerritory.gameObject;
        }
        
        return null;
    }
    
    public GameObject GetBorderFromPointB(){
         RaycastHit2D hit = Physics2D.Raycast(raycastPointB, Vector2.right);
        if(hit.collider.gameObject.TryGetComponent<ExpandTerritory>(out ExpandTerritory expandTerritory)){
            return expandTerritory.gameObject;
        }
        
        return null;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawRay(raycastPointB,Vector2.right);
        Gizmos.DrawWireSphere(territoryPoints.pointA.position, 5f);
        Gizmos.DrawWireSphere(territoryPoints.pointB.position, 5f);
    }
}
