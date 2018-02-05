using UnityEngine;
using System.Collections;

public class EnemyMovementZombunny : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator _animator;
    public float distanceToPlayer;
    public bool followingPlayer;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        _animator = GetComponent<Animator>();


    }

    private void Start()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
    }
    void Update ()
    {

        if (playerHealth.currentHealth <= 0)
            _animator.SetBool("PlayerDetected", false);

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Mathf.Abs(distanceToPlayer) <= 8)
        {

            followingPlayer = true;

        }
        
        if (Mathf.Abs(distanceToPlayer) > 8)
        {
            followingPlayer = false;
        }

        if (followingPlayer)
            _animator.SetBool("PlayerDetected", true);

        else if (!followingPlayer)
        {
            _animator.SetBool("PlayerDetected", false);
        }

        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && followingPlayer)
        {
             nav.enabled = true;
             nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
