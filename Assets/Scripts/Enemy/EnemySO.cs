using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{

    public int maxHealth;
    public float sinkSpeed;
    public int scoreValue;
    public float timeBetweenAttacks;
    public int attackDamage;

}
