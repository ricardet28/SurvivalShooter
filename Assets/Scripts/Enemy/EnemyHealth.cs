using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;//when dies
    public int scoreValue = 10;
    public AudioClip deathClip;

    private int enemyId;
    


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
        //Debug.Log(this.gameObject.name);
        AsignEnemyId();
        Debug.Log("Enemy called " + this.gameObject.name + " with id: " + enemyId);
        IncreaseEnemyCounter();
        
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    private void AsignEnemyId()
    {
        string name = this.gameObject.name;
        switch (name)
        {
            case "Zombunny(Clone)":
                enemyId = 1;
                break;
            case "ZomBear(Clone)":
                enemyId = 2;
                break;
            case "Hellephant(Clone)":
                enemyId = 3;
                break;
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
        DecreaseEnemyCounter();
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

    private void IncreaseEnemyCounter()
    {
        switch (enemyId)
        {
            case 1:
                EnemyCounterManager.instance.zombunnyCounter++;
                EnemyCounterManager.instance.zombunnyCounterText.text = "Zombunny: " + EnemyCounterManager.instance.zombunnyCounter;
                break;
            case 2:
                EnemyCounterManager.instance.zombearCounter++;
                EnemyCounterManager.instance.zombearCounterText.text = "Zombear: " + EnemyCounterManager.instance.zombearCounter;
                break;
            case 3:
                EnemyCounterManager.instance.hellephantCounter++;
                EnemyCounterManager.instance.hellephantCounterText.text = "Hellephant: " + EnemyCounterManager.instance.hellephantCounter;
                break;
        }
    }
    private void DecreaseEnemyCounter()
    {
        switch (enemyId)
        {
            case 1:
                EnemyCounterManager.instance.zombunnyCounter--;
                EnemyCounterManager.instance.zombunnyCounterText.text = "Zombunny: " + EnemyCounterManager.instance.zombunnyCounter;
                break;
            case 2:
                EnemyCounterManager.instance.zombearCounter--;
                EnemyCounterManager.instance.zombearCounterText.text = "Zombear: " + EnemyCounterManager.instance.zombearCounter;
                break;
            case 3:
                EnemyCounterManager.instance.hellephantCounter--;
                EnemyCounterManager.instance.hellephantCounterText.text = "Hellephant: " + EnemyCounterManager.instance.hellephantCounter;
                break;
        }

    }
}
