using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Enemy smallEnemy;
    public Enemy mediumEnemy;
    public Enemy largeEnemy;

    public void Awake()
    {

    }


    public void SpawnEnemy(EnemyType enemyType,MovementPattern movementPattern, Vector2 position)
    {
        switch (enemyType)
        {
            case EnemyType.small:
                Enemy small = GameObject.Instantiate<Enemy>(smallEnemy, position, Quaternion.identity);
                small.movementPattern = movementPattern;
                break;
            case EnemyType.medium:
                Enemy medium = GameObject.Instantiate<Enemy>(mediumEnemy, position, Quaternion.identity);
                medium.movementPattern = movementPattern;
                break;
            case EnemyType.large:
                Enemy large = GameObject.Instantiate<Enemy>(largeEnemy, position, Quaternion.identity);
                large.movementPattern = movementPattern;
                break;
            default:
                break;

        }

    }



}