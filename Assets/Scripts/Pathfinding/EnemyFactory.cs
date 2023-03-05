using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public enum EnemyType { Default, Follower, Chaser, Shooter }
    
    public Enemy CreateEnemy(EnemyType type)
    {
        Enemy enemy = new Enemy();
        if (type == EnemyType.Chaser || type == EnemyType.Follower)
            enemy.gameObject.AddComponent<CatchPlayer>();
        return enemy;
    }

}
