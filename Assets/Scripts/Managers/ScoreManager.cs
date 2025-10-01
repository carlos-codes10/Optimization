using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    [SerializeField] Text text;


    private void Start()
    {
        score = 0;
        text.text = "Score: " + score;
    }

    private void Update()
    {
        text.text = "Score: " + score;
    }


    public void UpdateScore(int points)
    {
        score += points;
        text.text = "Score: " + score;
    }
}
