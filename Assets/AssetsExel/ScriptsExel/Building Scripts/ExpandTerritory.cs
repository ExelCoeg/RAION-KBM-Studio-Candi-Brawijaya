
using UnityEngine;

public class ExpandTerritory : MonoBehaviour
{   
    public void Expand(){
        // panggil fungsi 
        if(gameObject.transform.position.x >= TerritoryManager.instance.pointBx){
            GameObject nearestDeadTreeB = TerritoryManager.instance.GetNearestDeadTreeWithExpandTerritoryFromPointB();
            // print(nearestDeadTreeB.transform.position.x);
            TerritoryManager.instance.territoryPoints.pointB.position = new Vector2(nearestDeadTreeB.transform.position.x, //
            TerritoryManager.instance.territoryPoints.pointB.position.y);
        }
        else if(gameObject.transform.position.x <= TerritoryManager.instance.pointAx){
            GameObject nearestDeadTreeA = TerritoryManager.instance.GetNearestDeadTreeWithExpandTerritoryFromPointA();
            // print(nearestDeadTreeA.transform.position.x);
            TerritoryManager.instance.territoryPoints.pointA.position = new Vector2(nearestDeadTreeA.transform.position.y , //
            TerritoryManager.instance.territoryPoints.pointA.position.y);
        }
        enabled = false;
    }
}
