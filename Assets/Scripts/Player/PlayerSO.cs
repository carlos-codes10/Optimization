using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
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
