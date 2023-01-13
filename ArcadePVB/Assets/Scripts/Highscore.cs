using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscore;
    public SaveManager saveManager;
    string[] highscores = new string[10];
    // Start is called before the first frame update
    void Start()
    {
        if (saveManager)
            DisplayHighscore();
    }
    public void DisplayHighscore()
    {
        highscores = saveManager.GetHighscores();
        highscore.text = "";
        for (int i = 0; i < highscores.Length; i++)
        {

            highscore.text += $"{i + 1}. {highscores[i]} \n";

        }
     
    }
    // Update is called once per frame
    void Update()
    {

    }
}
