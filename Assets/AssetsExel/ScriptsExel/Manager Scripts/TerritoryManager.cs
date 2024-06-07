
using UnityEngine;

public class TerritoryManager : MonoBehaviour
{

    public static TerritoryManager instance;
    public Points territoryPoints;
    public float pointAx,pointBx;

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
    
    public GameObject GetNearestDeadTreeWithExpandTerritoryFromPointA(){
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(territoryPoints.pointA.position.x - 10f,territoryPoints.pointA.position.y), Vector2.left);
        if(hit.collider.gameObject.TryGetComponent<ExpandTerritory>(out ExpandTerritory expandTerritory)){
            return expandTerritory.gameObject;
        }
        
        return null;
    }
    public GameObject GetNearestDeadTreeWithExpandTerritoryFromPointB(){
         RaycastHit2D hit = Physics2D.Raycast(new Vector2(territoryPoints.pointB.position.x + 10f,territoryPoints.pointB.position.y), Vector2.right);
        if(hit.collider.gameObject.TryGetComponent<ExpandTerritory>(out ExpandTerritory expandTerritory)){
            return expandTerritory.gameObject;
        }
        
        return null;
    }
}
