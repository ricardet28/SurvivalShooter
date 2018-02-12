using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private CounterCurrentEnemies counterCurrentEnemies;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    private void Awake()
    {
        counterCurrentEnemies = GetComponent<CounterCurrentEnemies>();
    }
    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    
    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        if (enemy.CompareTag("Zoombunny"))
        {
            counterCurrentEnemies.zoombunnyCounter++;
            counterCurrentEnemies.UpdateCounterTexts();

        }
        else if (enemy.CompareTag("Zoombear"))
        {
            counterCurrentEnemies.zoombearCounter++;
            counterCurrentEnemies.UpdateCounterTexts();

        }
        else if (enemy.CompareTag("Hellephant"))
        {
            counterCurrentEnemies.hellephantCounter++;
            counterCurrentEnemies.UpdateCounterTexts();

        }
    }
}
