using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyController> enemyPools;
    [SerializeField]
    private List<Transform> spawnLocators;
    [SerializeField]
    private float spawnTime;
    
    private GameData gameData;

    public bool isSpawn;
    public void Init(GameData gameData)
    {
        this.gameData = gameData;
        Observable.Interval(TimeSpan.FromSeconds(spawnTime))
            .Where(isPause => !gameData.isGameEnd)
            .Where(x => isSpawn)
            .Subscribe(t => SpawnEnemy());
    }

    private void SpawnEnemy()
    {
        var enemy = GetRandomItem(enemyPools);
        var locator = GetRandomItem(spawnLocators);
        EnemyController instance = Instantiate(enemy,locator);
        instance.gameObject.SetActive(true);
        instance.Init(gameData);
    }

    private T GetRandomItem<T>(List<T> Tlist)
    {
        return Tlist[UnityEngine.Random.Range(0, Tlist.Count)];
    }
   
}
