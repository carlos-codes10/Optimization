using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] PlayerSO playerStats;
    [SerializeField] NavMeshAgent agent;
    EnemyHealth enemyHealth;

    // THIS IS THE HEAVIEST UPDATE ON THE GAME FIX IT YOU CAN ALREADY TELL THIS IS BAD LOL
    // WE NEED SERIALIZE FIELD ON EVERYTHING NAV MESH AND PLAYER
    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement>().transform;
        enemyHealth = FindAnyObjectByType<EnemyHealth>();
    }
    void Update ()
    {
        // SCRIPTABLE OBJECT ENEMY & PLAYER HEALTH HERE
        if (enemyHealth.currentHealth > 0) 
        {
            if (playerStats.currentHealth > 0)
            {
                if (agent.isOnNavMesh)
                    agent.SetDestination(target.position);
            }
            else
            {
                agent.enabled = false;
            }

        }
    }
}
