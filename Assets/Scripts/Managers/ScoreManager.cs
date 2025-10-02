using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreSO myScore;


    [SerializeField] Text text;


    private void Start()
    {
        myScore.score = 0;  
        text.text = "Score: " + myScore.score;
    }

    private void Update()
    {
        text.text = "Score: " + myScore.score;
    }
}
