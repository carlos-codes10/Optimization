using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
    public float sinkSpeed;
    public float scoreValue;
    public float timeBetweenAttacks;
    public int attackDamage;

    private float minHealth = 0;

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, minHealth, maxHealth);
    }
}
