using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Animator _anim;
    [HideInInspector]public Transform player;
    PlayerHealth enemyHealth;//health of the human (the enemy)
    EnemyHealth playerHealth;//health of the zomby (the player)
    UnityEngine.AI.NavMeshAgent nav;
    public float maxDistance;
    [HideInInspector]public bool closeEnough = false;

    float lerpValue;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Zombunny").transform;
        enemyHealth = GetComponent <PlayerHealth> ();//health of the human (the enemy)
        playerHealth = player.GetComponent <EnemyHealth> ();//health of the zomby (the player)
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        _anim = GetComponent<Animator>();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (GetDistancePlayer() > maxDistance)
            {
                closeEnough = false;
                nav.isStopped = false;
                _anim.SetBool("IsWalking", true);
                nav.SetDestination(player.position);
                lerpValue = 0f;
            }
            else
            {
                AimToPlayer();
                closeEnough = true;
                nav.isStopped = true;
            }
            
        }
        else
        {
            closeEnough = false;
            _anim.SetBool("IsWalking", false);
            nav.enabled = false;
        }
    }

    private float GetDistancePlayer()
    {
        return Vector3.Distance(this.transform.position, player.transform.position);
    }

    private void AimToPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, lerpValue);
        lerpValue += Time.deltaTime;
    }

}
