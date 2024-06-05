using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour

{
    public static SpawnManager instance;
    public EnemyManager enemyManager;
    public DayManager dayManager;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private int[] lemulutWaveData;
    [SerializeField] private int[] gelapariWaveData;
    [SerializeField] private int[] widaraWaveData;

    Boolean enemyLeft;

    [SerializeField] int lemulutCount;
    [SerializeField] int gelapariCount;
    [SerializeField] int widaraCount;

    [SerializeField] int lemulutSpawnTime;
    [SerializeField] int gelapariSpawnTime;
    [SerializeField] int widaraSpawnTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        dayManager = DayManager.instance;
        enemyManager = EnemyManager.instance;
    }
    private void Update()
    {
        enemySpawn();
    }

    public Boolean isEnemyLeft()
    {
        return lemulutCount + gelapariCount + widaraCount > 0;
    }

    public void WavesSet(int waveCount)//start from 0
    {   
        if (lemulutWaveData.Length >= waveCount ){ lemulutCount = lemulutWaveData[waveCount];}else lemulutCount = 0;
        if (gelapariWaveData.Length >= waveCount){gelapariCount = gelapariWaveData[waveCount];}else gelapariCount = 0;
        if (widaraWaveData.Length >= waveCount){widaraCount = widaraWaveData[waveCount];}else widaraCount = 0;
          
        if (lemulutCount > 0)
        {
            lemulutSpawnTime = DayManager.instance.dayTime / lemulutCount;
        }
        else
        {
            lemulutSpawnTime = DayManager.instance.dayTime + 10;
        }

        if (gelapariCount > 0)
        {
            gelapariSpawnTime = DayManager.instance.dayTime / gelapariCount;
        }
        else
        {
            gelapariSpawnTime = DayManager.instance.dayTime + 10;
        }

        if (widaraCount > 0)
        {
            widaraSpawnTime = DayManager.instance.dayTime / widaraCount;
        }
        else
        {
            widaraSpawnTime = DayManager.instance.dayTime + 10;
        }

    }

    public void enemySpawn()
    {
        if (lemulutCount > 0 && dayManager.clock > lemulutSpawnTime)
        {
            lemulutSpawnTime += lemulutSpawnTime;
            SpawnLemulut();
            lemulutCount--;
        }
        if (gelapariCount > 0 && dayManager.clock > gelapariSpawnTime)
        {
            gelapariSpawnTime += gelapariSpawnTime;
            SpawnGelapari();
            gelapariCount--;
        }
        if (widaraCount > 0 && dayManager.clock > widaraSpawnTime)
        {
            widaraSpawnTime += widaraSpawnTime;
            SpawnWidara();
            widaraCount--;
        }
    }
    public void SpawnLemulut()
    {
        enemyManager.InstanceEnemy(EnemyStatus.Lemulut, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Lemulut, rightSpawnPoint.position);
    }
    public void SpawnGelapari()
    {
        enemyManager.InstanceEnemy(EnemyStatus.Gelapari, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Gelapari, rightSpawnPoint.position);
    }
    public void SpawnWidara()
    {
        enemyManager.InstanceEnemy(EnemyStatus.Widara, leftSpawnPoint.position);
        enemyManager.InstanceEnemy(EnemyStatus.Widara, rightSpawnPoint.position);
    }
}
