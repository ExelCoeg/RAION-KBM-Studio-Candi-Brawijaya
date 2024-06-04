using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public EnemyManager enemyManager;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] int[][] waveData;

    Boolean enemyLeft;

    int lemulutCount;
    int gelapariCount;
    int widaraCount;

    int lemulutSpawnTime;
    int gelapariSpawnTime;
    int widaraSpawnTime;

    [SerializeField] private float clock;
    private void Awake() {
        if (instance == null)
        {   
            instance = this;
        }
    }

    private void Start() {
        enemyManager = EnemyManager.instance;
    }
    private void Update() {
        clock = 0;//nanti samakan dengan daymanager
    }

    public Boolean isEnemyLeft(){
        return lemulutCount + gelapariCount + widaraCount > 0;
    }

    public void wavesSet(int waveCount, int dayTime)//start from 0
    {
        lemulutCount = waveData[waveCount][0];
        gelapariCount = waveData[waveCount][1];
        widaraCount = waveData[waveCount][2];   

        widaraSpawnTime = lemulutCount;// di set nanti sesuai waktu 
        lemulutSpawnTime = gelapariCount;
        gelapariSpawnTime = widaraCount;
    }

    public void enemySpawn(){
        if(lemulutCount > 0 && clock > lemulutSpawnTime){  
            lemulutSpawnTime += lemulutSpawnTime;
            SpawnLemulut();
            lemulutCount--;
        }
        if(gelapariCount > 0 && clock > gelapariSpawnTime){  
            gelapariSpawnTime += gelapariSpawnTime;
            SpawnGelapari();
            gelapariCount--;
        }
        if(widaraCount > 0 && clock > widaraSpawnTime){  
            widaraSpawnTime += widaraSpawnTime;
            SpawnWidara();
            widaraCount--;
        }
    }
    public void SpawnLemulut(){
        enemyManager.InstanceEnemy(EnemyStatus.Lemulut, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Lemulut, rightSpawnPoint.position);
    } 
    public void SpawnGelapari(){
        enemyManager.InstanceEnemy(EnemyStatus.Gelapari, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Gelapari, rightSpawnPoint.position);  
    } 
    public void SpawnWidara(){
        enemyManager.InstanceEnemy(EnemyStatus.Widara, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Widara, rightSpawnPoint.position);
    } 
}
