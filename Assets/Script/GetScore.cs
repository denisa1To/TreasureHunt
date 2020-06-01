using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    private readonly string scoreResult = "Score";
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt(scoreResult);
	ScoreText.text = "Score: "+ score;
    }

    // Update is called once per frame
    void Update()
    {
        int score = PlayerPrefs.GetInt(scoreResult);
	ScoreText.text = "Score: "+ score;
    }

   
   
}
