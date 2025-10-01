using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool isShooting;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if (isShooting)
        {
            AttemptFire();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }

    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void OnShoot(InputValue v)
    {
        // code here for new input system basically the same code
        // you need to figure out how to do the time between bullets inside here
        Debug.Log("ONSHOOT IS CALLED");

        if (v.isPressed)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
        
         
    }

    void AttemptFire()
    {
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
            timer = 0;
        }

    }

}
