using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayB()
    {
        SceneManager.LoadScene("Level1");
    }
    public void CreditsB()
    {
        //credits things
    }
    public void Quit()
    {
        Application.Quit();
    }
   
}
