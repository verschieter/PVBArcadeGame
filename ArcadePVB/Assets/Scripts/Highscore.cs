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

        highscore.text = $"1. {highscores[0]} \n" +
            $"2. {highscores[1]} \n" +
            $"3. {highscores[2]} \n" +
            $"4. {highscores[3]} \n" +
            $"5. {highscores[4]} \n" +
            $"6. {highscores[5]} \n" +
            $"7. {highscores[6]} \n" +
            $"8. {highscores[7]} \n" +
            $"9. {highscores[8]} \n" +
            $"10. {highscores[9]} \n";
    }


    // Update is called once per frame
    void Update()
    {

    }
}
