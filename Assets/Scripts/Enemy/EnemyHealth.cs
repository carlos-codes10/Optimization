using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking;
    int id_dead = Animator.StringToHash("Dead");
    EnemyManager pool;

    NavMeshAgent agent;
    Rigidbody rb;

    // Scritable Object
    [SerializeField] PlayerSO playerStats;
    [SerializeField] EnemySO enemyStats;
    [SerializeField] ScoreSO myScore;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        rb = GetComponent <Rigidbody> ();
        agent = GetComponent <NavMeshAgent> ();

    }
    private void Start()
    {
        currentHealth = enemyStats.maxHealth;
    }

    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * enemyStats.sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger (id_dead);

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    private void OnEnable()
    {
        StartCoroutine(EnableDelay(0.2f));
        currentHealth = enemyStats.maxHealth;
        rb.isKinematic = false;
        isSinking = false;
        isDead = false;
        capsuleCollider.isTrigger = false;

    }
    private void OnDisable()
    {
        if (pool != null)
        {
            
            pool.AddToQueue(this);
        }
    }

    public void SetPool(EnemyManager bp)
    {
        pool = bp;
    }


    public void StartSinking ()
    {
        agent.enabled = false;
        rb.isKinematic = true;
        isSinking = true;
        myScore.score += enemyStats.scoreValue;
        StartCoroutine(DisableAfterDelay(2f));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private IEnumerator EnableDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.enabled = true;
    }
}
