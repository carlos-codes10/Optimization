using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform player;
   // [SerializeField] PlayerSO playerSO; // DONT FORGET SCRIPTABLE OBJECT FOR PLAYER
   // [SerializeField] EnemySO enemySO; // DONT FORGET SCRIPTABLE OBJECT FOR ENEMY
    [SerializeField] NavMeshAgent agent;

    // THIS IS THE HEAVIEST UPDATE ON THE GAME FIX IT YOU CAN ALREADY TELL THIS IS BAD LOL
    // WE NEED SERIALIZE FIELD ON EVERYTHING NAV MESH AND PLAYER
    void Update ()
    {
        // SCRIPTABLE OBJECT ENEMY & PLAYER HEALTH HERE
        if (GetComponent<EnemyHealth>().currentHealth > 0 && player.GetComponent<PlayerHealth>().currentHealth > 0) 
        {
            Debug.Log("GETTING PLAYER");
            agent.SetDestination(player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
