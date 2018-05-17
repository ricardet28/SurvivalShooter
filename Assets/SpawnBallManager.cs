using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallManager : MonoBehaviour {

    public float minTimeSpawn;
    public float maxTimeSpawn;
    private float timeSpawn;
    public int numBallsSpawned;
    private float currentTime;

    private float maxX = 15f;
    private float minX = -15f;
    private float maxZ = 15f;
    private float minZ = -15f;

    public GameObject sphere;

    public static SpawnBallManager instance = null;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {

        timeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);

	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= timeSpawn && numBallsSpawned<3)
        {
            SpawnSphere();
        }
	}

    private void SpawnSphere()
    {
        Debug.Log("SPAWN SPHERE");
        Instantiate(sphere, new Vector3(Random.Range(minX, maxX), 0.35f, Random.Range(minZ, maxZ)), Quaternion.identity);
        numBallsSpawned++;
        currentTime = 0f;
        timeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
    }
}
