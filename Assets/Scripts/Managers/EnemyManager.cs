using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    //public GameObject enemy;
    [SerializeField] EnemyHealth enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    // start enemy pool
    [SerializeField] int enemyAmount;
    Queue<EnemyHealth> remainingEnemies = new Queue<EnemyHealth>();


    void Start ()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            var e =  Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            e.SetPool(this);
            e.gameObject.SetActive(false);
            
        }
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {

    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        if (remainingEnemies.Count > 0)
        {
            var current = remainingEnemies.Dequeue();
            current.gameObject.SetActive(true);

            // spawn location
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            current.transform.position = spawnPoints[spawnPointIndex].position;
        }
    }

    public void AddToQueue(EnemyHealth enemy)
    {
        remainingEnemies.Enqueue(enemy);
    }
}
