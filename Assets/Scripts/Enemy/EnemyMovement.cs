using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    SafeZone safeZone;
    GameObject spawnAreaZoombunny;
    GameObject spawnAreaZoombear;
    GameObject spawnAreaHellephant;



    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        safeZone = GameObject.FindGameObjectWithTag("SafeZone").GetComponent<SafeZone>();
        spawnAreaZoombunny = GameObject.FindGameObjectWithTag("SpawnZoombunny");
        spawnAreaZoombear = GameObject.FindGameObjectWithTag("SpawnZoombear");
        spawnAreaHellephant = GameObject.FindGameObjectWithTag("SpawnHellephant");

    }

    private void Start()
    {
        
    }
    void Update ()
    {

        

        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.enabled = true;
            if (safeZone.playerInside)
            {
                if (this.gameObject.CompareTag("Zoombunny"))
                {
                    nav.SetDestination(spawnAreaZoombunny.transform.position);
                }
                else if (this.gameObject.CompareTag("Zoombear"))
                {
                    nav.SetDestination(spawnAreaZoombear.transform.position);
                }
                else if (this.gameObject.CompareTag("Hellephant"))
                {
                    nav.SetDestination(spawnAreaHellephant.transform.position);
                }


            }
                
            else
                nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
