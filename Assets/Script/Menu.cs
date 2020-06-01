using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private string scoreResult = "Score";
    public Text ScoreText;

    // Start is called before the first frame update
    public void Start()
    {

        int score = PlayerPrefs.GetInt(scoreResult);
        ScoreText.text = "Score: " + score;
    }


    public void tryAgain()
    {
        SceneManager.LoadScene(1);

    }

    public void exit()
    {
        Application.Quit();
    }

 	
}
