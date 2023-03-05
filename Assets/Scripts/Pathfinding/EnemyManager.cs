using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyFactory factory;
    private int maxEnemyCount;
    private List<Enemy> enemies;

    public List<Transform> spawnPoints;
    public MapGrid curMapGrid;
    private void Update()
    {
        if (enemies.Count < maxEnemyCount)
        {
            while (maxEnemyCount - enemies.Count > 0)
            {
                Enemy enemy = factory.CreateEnemy(EnemyFactory.EnemyType.Default + Random.Range(1, 4));
                CatchPlayer catchPlayer = enemy.GetComponent<CatchPlayer>();
                if (catchPlayer != null)
                {
                    catchPlayer.mapGrid = curMapGrid;
                }
                enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                enemies.Add(enemy);
            }
        }
    }

    public void DestroyEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}
