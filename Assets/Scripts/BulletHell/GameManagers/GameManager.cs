using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public  BulletHellUIManager uIManager;
    public PlayerManager playerManager;
    public EnemyManager enemyManager;

    public Level level;
    void Awake(){
        instance = this;
        
    }
}
