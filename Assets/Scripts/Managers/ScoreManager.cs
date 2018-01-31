using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;//static means that this variable belongs to the class itself and not to their instances.


    Text _text;


    void Awake ()
    {
        _text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        _text.text = "Score: " + score;
    }
}
