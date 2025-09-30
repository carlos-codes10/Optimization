using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    int id_dead = Animator.StringToHash("Dead");
    EnemyManager pool;

    NavMeshAgent agent;
    Rigidbody rb;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        rb = GetComponent <Rigidbody> ();
        agent = GetComponent <NavMeshAgent> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
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
        agent.enabled = true;
        rb.isKinematic = false;
        isSinking = false;
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
        ScoreManager.score += scoreValue;
        StartCoroutine(DisableAfterDelay(2f));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
