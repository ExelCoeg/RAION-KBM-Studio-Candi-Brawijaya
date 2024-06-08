
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
        raycastPointA = territoryPoints.pointA.position;
        raycastPointB = territoryPoints.pointB.position;

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
        GameObject grassBorder;
        RaycastHit2D hit = Physics2D.Raycast(raycastPointA, Vector2.left,100,LayerMask.GetMask("GrassBorder"));
        if(hit.collider.CompareTag("GrassBorder")){
            grassBorder = hit.collider.gameObject;
        }
        else{
            grassBorder = null;
        }
        return grassBorder;
    }
    
    public GameObject GetBorderFromPointB(){    
        GameObject grassBorder;
         RaycastHit2D hit = Physics2D.Raycast(raycastPointB, Vector2.right, 100,LayerMask.GetMask("GrassBorder"));
        //  print(hit.collider.gameObject.name);
        if(hit.collider.CompareTag("GrassBorder")){
            grassBorder = hit.collider.gameObject;
        }
        else{
            grassBorder = null;
        }
        return grassBorder;
    
       
    }
    // private void OnDrawGizmos() {
    //     Gizmos.DrawRay(raycastPointB, Vector2.right);
    //     Gizmos.DrawWireSphere(territoryPoints.pointA.position, 5f);
    //     Gizmos.DrawWireSphere(territoryPoints.pointB.position, 5f);
    // }
}
