using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChangeFertile : MonoBehaviour
{
    private TerritoryManager territoryManager;
    [SerializeField] private bool inTerritory;
    private void Start() {
        territoryManager = TerritoryManager.instance;
    }
    void Update()
    {
        // TerritoryManager.instance.territoryPoints.pointA
        if (territoryManager.territoryPoints.pointA.position.x < transform.position.x && territoryManager.territoryPoints.pointB.position.x> transform.position.x)
        {
            inTerritory = true;
        }else{
            inTerritory = false;
        }
        gameObject.GetComponent<Animator>().SetBool("inTerritory",inTerritory);
    }
}
