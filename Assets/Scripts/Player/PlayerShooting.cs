using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    private int originalDamagePerShot;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public float timePowerShotAvalaible = 5f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool powerShotAvalaible = true;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        originalDamagePerShot = damagePerShot;
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
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

        gunParticles.Stop ();//if they are already playing.
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);//set the vertex 0 on the barrel.

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;//direction of the gun barrel

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))//if we collide something with shootalbleMask mask inside the range.
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)//if exists
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);//on the variable shootHit we store the point where we collide the shootRay on the object with shootableMask.
            }
            gunLine.SetPosition (1, shootHit.point);//a shootable object can be a wall, an enemy, etc. SO we need this out of the above if condition.
        }
        else//if we do not collide we set the end of the line in the maximum range.
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            SpawnBallManager.instance.numBallsSpawned--;
            Destroy(collision.gameObject);
            if (powerShotAvalaible)
                StartCoroutine(PowerShotStillAvalaible());
        }
    }

    private IEnumerator PowerShotStillAvalaible()
    {
        damagePerShot = 1000;
        float currentTime = 0f;
        while (currentTime <= timePowerShotAvalaible)
        {
            powerShotAvalaible = false;
            Debug.Log("ULTRA SHOT ENABLED!!!");
            currentTime += Time.deltaTime;
            yield return null;
        }
        damagePerShot = originalDamagePerShot;
        powerShotAvalaible = true;
    }
}
