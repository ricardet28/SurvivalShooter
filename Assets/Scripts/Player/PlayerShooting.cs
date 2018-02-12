using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float boostedDamagePerShot = Mathf.Infinity;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    public bool boostedShoot;
    public float timeBoostedShoot = 5f;
    private float currentTimeBoostedShoot;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public Material gunLineBoostedMaterial;
    public Material gunLineMaterial;

    void Awake ()
    {
        boostedShoot = false;
        currentTimeBoostedShoot = 0f;
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        
        gunLine = GetComponent<LineRenderer>();
        gunLine.material = gunLineMaterial;

        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }
   

    void Update ()
    {
        Debug.Log(boostedShoot);
        CheckIfBoostedShoot();
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            if (boostedShoot)
                ShootBoosted();
            else
                Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

    public void CheckIfBoostedShoot()
    {
        if (boostedShoot)
        {
            Debug.Log("boostedShot enabled");
            gunLine.material = gunLineBoostedMaterial;
            gunLight.color = Color.red;
            currentTimeBoostedShoot += Time.deltaTime;
            if (currentTimeBoostedShoot >= timeBoostedShoot)
            {
                boostedShoot = false;
                Debug.Log("boostedShot disabled");
                gunLine.material = gunLineMaterial;
                currentTimeBoostedShoot = 0f;
            }
        }
    }
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void ShootBoosted()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;
        //gunLight.color = Color.red;
        gunParticles.Stop();//if they are already playing.
        gunParticles.Play();

        gunLine.enabled = true;
        
        gunLine.SetPosition(0, transform.position);//set the vertex 0 on the barrel.

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;//direction of the gun barrel

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))//if we collide something with shootalbleMask mask inside the range.
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)//if exists
            {
                
                enemyHealth.TakeDamage((int)boostedDamagePerShot, shootHit.point);//on the variable shootHit we store the point where we collide the shootRay on the object with shootableMask.
               
            }
            gunLine.SetPosition(1, shootHit.point);//a shootable object can be a wall, an enemy, etc. SO we need this out of the above if condition.
        }
        else//if we do not collide we set the end of the line in the maximum range.
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;
        //gunLight.color = Color.yellow;
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
               
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);

            }
            gunLine.SetPosition (1, shootHit.point);//a shootable object can be a wall, an enemy, etc. SO we need this out of the above if condition.
        }
        else//if we do not collide we set the end of the line in the maximum range.
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
