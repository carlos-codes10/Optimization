using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    int id_pdead = Animator.StringToHash("PlayerDead");

    //Scriptable Object
    [SerializeField] PlayerSO playerStats;
    [SerializeField] EnemySO enemyStats;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= enemyStats.timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerStats.currentHealth <= 0)
        {
            anim.SetTrigger (id_pdead);
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerStats.currentHealth > 0)
        {
            playerHealth.TakeDamage(enemyStats.attackDamage);
        }
    }
}
