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
        if (DayManager.instance.isNight && !isOn)
        {
            Invoke("On", delay);
            isOn = true;
        }else if(!DayManager.instance.isNight && isOn){
            Off();
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
