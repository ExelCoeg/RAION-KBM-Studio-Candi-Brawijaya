
using UnityEngine;

public class ExpandTerritory : MonoBehaviour
{   
   
    public void Expand(){
        // panggil fungsi 
        if(TerritoryManager.instance.onPointB){
            GameObject nearestBorderB = TerritoryManager.instance.GetBorderFromPointB();
            print("nearestBorderB: " + nearestBorderB.transform.position);
            TerritoryManager.instance.territoryPoints.pointB.position = new Vector2(nearestBorderB.transform.position.x, transform.position.y-2f);
            enabled = false;
        }
        else if(TerritoryManager.instance.onPointA){
            GameObject nearestBorderA = TerritoryManager.instance.GetBorderFromPointA();
            print("nearestBorderA: "  + nearestBorderA.transform.position);
            TerritoryManager.instance.territoryPoints.pointA.position = new Vector2(nearestBorderA.transform.position.x,  transform.position.y-2f);
            enabled = false;
        }
    }
}

        
        