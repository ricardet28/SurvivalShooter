using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    private Animator animator;
    public bool detectPlayer;
    // Use this for initialization
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //follows him
            detectPlayer = true;
            animator.SetBool("PlayerDetected", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            detectPlayer = false;
            animator.SetBool("PlayerDetected", false);
        }
    }
}
