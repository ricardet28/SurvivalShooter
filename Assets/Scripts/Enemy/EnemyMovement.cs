using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    PlayerMovement playerMovement;
    UnityEngine.AI.NavMeshAgent nav;
    Vector3 positionSpawnArea;
    Quaternion rotationSpawnArea;

    public float rotationSpeed = 0.5f;
    public float maxDistance = 10f;


    void Awake ()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        positionSpawnArea = new Vector3(transform.position.x, 0, transform.position.z);
        rotationSpawnArea = transform.rotation;
        Debug.Log("Spawn area: " + positionSpawnArea);
    }

    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {

            if (playerMovement.playerInSafeArea)
            {
                nav.enabled = true;
                nav.SetDestination(positionSpawnArea);
                anim.SetBool("Move", true);
                if (Vector3.Distance(transform.position,positionSpawnArea) < 0.5f)
                {
                    nav.enabled = false;
                    anim.SetBool("Move", false);
                    StartCoroutine(SetInitialRotation());
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.position) <= maxDistance)
                {
                    nav.enabled = true;
                    nav.SetDestination(player.position);
                    anim.SetBool("Move", true);
                }
                else
                {
                    nav.enabled = false;
                    anim.SetBool("Move", false);
                }
            }
            
            
        }
        else
        {
            nav.enabled = false;
        }
    }

    private IEnumerator SetInitialRotation()
    {
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationSpawnArea, lerpValue);
            lerpValue += Time.deltaTime * rotationSpeed;
            yield return null;
        }
    }
}
