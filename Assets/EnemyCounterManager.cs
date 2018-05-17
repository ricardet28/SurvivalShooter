using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterManager : MonoBehaviour {

    public static EnemyCounterManager instance = null;

    public Text zombunnyCounterText;
    public Text zombearCounterText;
    public Text hellephantCounterText;

    public int zombunnyCounter;
    public int zombearCounter;
    public int hellephantCounter;


    private void Awake()
    {
        instance = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
