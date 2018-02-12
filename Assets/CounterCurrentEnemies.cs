using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterCurrentEnemies : MonoBehaviour {

    [HideInInspector] public int zoombunnyCounter;
    [HideInInspector] public int zoombearCounter;
    [HideInInspector] public int hellephantCounter;

    public Text zoombunnyCounterText;
    public Text zoombearCounterText;
    public Text hellephantCounterText;

    private void Awake()
    {


        zoombunnyCounterText.text = "Zoombunny: 0";
        zoombunnyCounterText.text = "Zoombear: 0";
        zoombunnyCounterText.text = "Hellephant: 0";

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateCounterTexts()
    {
        zoombunnyCounterText.text = "Zoombunny: " + zoombunnyCounter;
        zoombearCounterText.text = "Zoombear: " + zoombearCounter;
        hellephantCounterText.text = "Hellephant: " + hellephantCounter;
    }
}
