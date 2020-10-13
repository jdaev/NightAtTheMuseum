using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveScriptableObject", order = 1)]
public class SpawnPackage : ScriptableObject
{
    public EnemyType enemy;

    public MovementPattern movementType;
    public float timeSpacing;
    public int numberToSpawn;
}
