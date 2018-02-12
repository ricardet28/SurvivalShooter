using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenSphereManager : MonoBehaviour {

    public GameObject spherePU;
    private GameObject auxGO;
    private float minX = -10;
    private float maxX = 10;
    private float minZ = -10;
    private float maxZ = 10;

    public float timeToSpawn = 3f;
    private float currentTimer;
    public bool spawned;
    public int currentSpheresSpawned = 0;
    // Use this for initialization
    private void Awake()
    {
        spawned = false;
        currentTimer = 0f;
    }
    void Start () {
        currentSpheresSpawned = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer += Time.deltaTime;
        if (currentSpheresSpawned < 3 && currentTimer >= timeToSpawn)
        {
            SpawnSphere();
            currentTimer = 0f;
            timeToSpawn = Random.Range(3f, 7f);
            
            //Instantiate(spherePU, new Vector3(-8f, 0f, 12f), Quaternion.identity);


        }
	}
    void SpawnSphere()
    {
        Debug.Log("spawn sphere");
        currentSpheresSpawned++;
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
        Instantiate(spherePU, spawnPosition, Quaternion.identity);
        
    }

    public void CanSpawnAnother()
    {
        currentTimer = 0f;
        timeToSpawn = Random.Range(3f, 7f);
        currentSpheresSpawned--;
    }

}
