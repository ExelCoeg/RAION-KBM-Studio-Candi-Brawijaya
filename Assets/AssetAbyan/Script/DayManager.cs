using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class DayManager : MonoBehaviour
{   
    
    public static DayManager instance;
    [SerializeField] public int dayCount;
    [Header("TIME")]
    [SerializeField] public float clock;
    [SerializeField] public int dayTime;
    [SerializeField] public int nightIime;
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
        SpawnManager.instance.WavesSet(dayCount);
    }
    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;
        if (clock > dayTime + nightIime){
            clock = 0;
            dayCount++;
            SpawnManager.instance.WavesSet(dayCount);
        }
        setNight();
    }
    public void setNight(){
        if (clock > dayTime)
        {
            isNight = true;
        }else if(isNight){
            isNight = false;
        }
    }

}
