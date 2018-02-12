using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;//when dies
    public int scoreValue = 10;
    public AudioClip deathClip;

    private CounterCurrentEnemies counterCurrentEnemies;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        counterCurrentEnemies = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<CounterCurrentEnemies>();
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

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
        if (this.gameObject.CompareTag("Zoombunny"))
        {
            counterCurrentEnemies.zoombunnyCounter--;
        }

        else if (this.gameObject.CompareTag("Zoombear"))
        {
            counterCurrentEnemies.zoombearCounter--;
        }
        else if (this.gameObject.CompareTag("Hellephant"))
        {
            counterCurrentEnemies.hellephantCounter--;
        }

        counterCurrentEnemies.UpdateCounterTexts();

        isDead = true;

        capsuleCollider.isTrigger = true;//when dies the enemy can walk throught them.

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
