
using UnityEngine;

public class ExpandTerritory : MonoBehaviour
{   
  
    public void Expand(){
        // panggil fungsi 
        if(TerritoryManager.instance.onPointB){
            Collider2D currentBorderB = Physics2D.OverlapCircle(TerritoryManager.instance.territoryPoints.pointB.position,5f,LayerMask.GetMask("GrassBorder"));
            if(currentBorderB != null){
                if(currentBorderB.TryGetComponent<GrassRightBorderScript>(out GrassRightBorderScript grassRightBorderScript)){
                    grassRightBorderScript.CallAnim();
                    grassRightBorderScript.CallEndAnim();
                }
                else print("currentBorderB has no GrassRightBorderScript component");
    
            }
            GameObject nearestBorderB = TerritoryManager.instance.GetBorderFromPointB();
            // nearestBorderB.gameObject.GetComponent<GrassRightBorderScript>().CallAnim();
            print("nearestBorderB: " + nearestBorderB.transform.position);
            TerritoryManager.instance.territoryPoints.pointB.position = new Vector2(nearestBorderB.transform.position.x + 5f, transform.position.y);
        }
        if(TerritoryManager.instance.onPointA){

            Collider2D currentBorderA = Physics2D.OverlapCircle(TerritoryManager.instance.territoryPoints.pointA.position,5f,LayerMask.GetMask("GrassBorder"));
            print("currentBorderA: " + currentBorderA.transform.position);
           
            
            if(currentBorderA != null){
                if(currentBorderA.TryGetComponent<GrassLeftBorderScript>(out GrassLeftBorderScript grassLeftBorderScript)){
                    grassLeftBorderScript.CallAnim();
                    grassLeftBorderScript.CallEndAnim();
                }
                else print("currentBorderA has no GrassLeftBorderScript component");
            }

            GameObject nearestBorderA = TerritoryManager.instance.GetBorderFromPointA();
            // nearestBorderA.gameObject.GetComponent<GrassLeftBorderScript>().CallAnim();

            print("nearestBorderA: "  + nearestBorderA.transform.position);

            TerritoryManager.instance.territoryPoints.pointA.position = new Vector2(nearestBorderA.transform.position.x - 5f,  transform.position.y);
        }
    }
}

        
        