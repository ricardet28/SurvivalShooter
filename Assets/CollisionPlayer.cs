using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour {
    private GreenSphereManager greenSphereManager;
    private PlayerShooting playerShooting;
    // Use this for initialization
    private void Awake()
    {
        greenSphereManager = GameObject.FindGameObjectWithTag("PowerupsManager").GetComponent<GreenSphereManager>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShooting>();

        
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            if (!playerShooting.boostedShoot)
            {
                playerShooting.boostedShoot = true;
                

            }
                
            greenSphereManager.CanSpawnAnother();
        }
    }
}
