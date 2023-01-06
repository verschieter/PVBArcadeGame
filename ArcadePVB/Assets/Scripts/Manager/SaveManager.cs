using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    string[] highscoreString = new string[10];
    int saveAmount;

    void Awake()
    {
        saveAmount = highscoreString.Length;
        //ResetHighscore();
        for (int i = 1; i <= saveAmount; i++)
        {
            string a = PlayerPrefs.GetString(i.ToString());

            if (string.IsNullOrEmpty(a))
                PlayerPrefs.SetString(i.ToString(), $"none 0");
        }
    }

    public void SetScore(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public void IsInHighscore( string name, int amount)
    {
        for (int i = 1; i <= saveAmount; i++)
        {
            string indexNameAndScore = PlayerPrefs.GetString(i.ToString());

            string[] splitArray = indexNameAndScore.Split(' ');
            int indexScore = int.Parse(splitArray[1]);
            if (amount > indexScore)
            {
                for (int index = 1; index <= saveAmount - i; index++)
                {
                    //replace the bottom score with the previous one
      
                    int newIndex = saveAmount - index;
                    string newName = PlayerPrefs.GetString(newIndex.ToString());
                    int oldIndex = newIndex + 1;
                    SetScore(oldIndex.ToString(), newName);
                }
                SetScore(i.ToString(), $"{name} {amount}");
                break;
            }
        }
    }
    //mostly for debugs
    public void ResetHighscore()
    {
        PlayerPrefs.DeleteAll();
    }

    public string[] GetHighscores()
    {
        for (int i = 1; i <= saveAmount; i++)
        {
            highscoreString[i - 1] = PlayerPrefs.GetString(i.ToString());
        }

        return highscoreString;
    }
}
