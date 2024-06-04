using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DayManager : MonoBehaviour
{   
    public static DayManager instance;

    [SerializeField] float clock;
    [SerializeField] int totalDayTime;
    [SerializeField] int nightIime;
    public bool isNight;


     
    // Start is called before the first frame update
    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;
        if (clock > totalDayTime){
            clock = 0;
        }
        setNight();

    }
    public void setNight(){
        if (clock > nightIime)
        {
            isNight = true;
        }else if(isNight){
            isNight = false;
        }
    }

}
