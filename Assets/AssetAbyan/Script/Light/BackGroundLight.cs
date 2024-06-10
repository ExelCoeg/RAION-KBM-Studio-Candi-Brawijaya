using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLight : MonoBehaviour
{
    
    [SerializeField]bool isOn;
    public float delay;
    void Update()
    {
        if (DayManager.instance.isNight && !isOn)
        {
            Invoke("On", delay);
            isOn = true;
        }else if(!DayManager.instance.isNight && isOn){
            Off();
        }
    }
    void On(){
        gameObject.GetComponent<Animator>().Play("SkyToNight");
    }
    void Off(){
        gameObject.GetComponent<Animator>().Play("SkyToDay");
        isOn = false;
    }
}
