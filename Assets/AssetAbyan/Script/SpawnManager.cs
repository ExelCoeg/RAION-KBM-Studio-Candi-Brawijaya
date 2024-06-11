using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] int vagrantSpawnTime;

    [SerializeField] int tempLemulutSpawnTime;
    [SerializeField] int tempGelapariSpawnTime;
    [SerializeField] int tempWidaraSpawnTime;
    [SerializeField] float tempVagrantSpawnTime;
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
        SpawnVagrant();
    }

    public Boolean isEnemyLeft()
    {
        return lemulutCount + gelapariCount + widaraCount > 0;
    }

    public void WavesSet(int waveCount)//start from 0
    {
        tempLemulutSpawnTime = 0;
        tempGelapariSpawnTime = 0;
        tempWidaraSpawnTime = 0;
        tempVagrantSpawnTime = 0;

        print("SETWAVE " + waveCount);
        if (lemulutWaveData.Length - 1>= waveCount)
        {
            lemulutCount = lemulutWaveData[waveCount];
        }
        else
        {
            lemulutCount = 0;
        }
        if (gelapariWaveData.Length - 1>= waveCount)
        {
            gelapariCount = gelapariWaveData[waveCount];
        }
        else
        {
            gelapariCount = 0;
        }
        if (widaraWaveData.Length - 1 >= waveCount)
        {
            widaraCount = widaraWaveData[waveCount];
        }
        else
        {
            widaraCount = 0;
        }

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
        if (lemulutCount > 0 && dayManager.clock > tempLemulutSpawnTime)
        {
            tempLemulutSpawnTime += lemulutSpawnTime;
            SpawnLemulut();
            lemulutCount--;
        }
        if (gelapariCount > 0 && dayManager.clock > tempGelapariSpawnTime)
        {
            tempGelapariSpawnTime += gelapariSpawnTime;
            SpawnGelapari();
            gelapariCount--;
        }
        if (widaraCount > 0 && dayManager.clock > tempWidaraSpawnTime)
        {
            tempWidaraSpawnTime += widaraSpawnTime;
            SpawnWidara();
            widaraCount--;
        }
    }

    public void SpawnVagrant()
    {
        tempVagrantSpawnTime += Time.deltaTime;
        if (tempVagrantSpawnTime >= vagrantSpawnTime)
        {
            if (PointManager.instance.GetPoint(PointsNames.LeftVagrant).NPCCount < 5)
            {
                NPCManager.instance.InstanceNPC(Status.Vagrant, PointManager.instance.GetPoint(PointsNames.LeftVagrant).pointA.position);
            }
            else if (PointManager.instance.GetPoint(PointsNames.RightVagrant).NPCCount < 5)
            {
                NPCManager.instance.InstanceNPC(Status.Vagrant, PointManager.instance.GetPoint(PointsNames.RightVagrant).pointB.position);
            }
            tempVagrantSpawnTime = 0;
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
