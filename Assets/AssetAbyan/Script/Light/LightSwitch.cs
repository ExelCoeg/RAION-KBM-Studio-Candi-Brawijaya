using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]string on;
    [SerializeField]string off;
    [SerializeField]bool isOn;
    [SerializeField]private float delay;
    void Update()
    {
        if (TerritoryManager.instance.territoryPoints.pointA.position.x < transform.position.x && TerritoryManager.instance.territoryPoints.pointB.position.x> transform.position.x)
        {
            if (DayManager.instance.isNight && !isOn)
            {
                Invoke("On", delay);
                isOn = true;
            }else if(!DayManager.instance.isNight && isOn){
                Off();
            }
        }

    }
    void On(){
        gameObject.GetComponent<Animator>().Play(on);
    }
    void Off(){
        gameObject.GetComponent<Animator>().Play(off);
        isOn = false;
    }
}
