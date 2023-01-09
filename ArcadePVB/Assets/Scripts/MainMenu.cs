using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject CreditsMenu;
    public GameObject PlayerSelectMenu;
    public void PlayerSelectB()
    {
        PlayerSelectMenu.SetActive(!PlayerSelectMenu.activeSelf);
    }


    public void Play(int amountOfPlayer)
    {
        GameManager.amountPlayer = amountOfPlayer;
        SceneManager.LoadScene("Level1");

    }


    public void CreditsB()
    {
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
    }
    public void Quit()
    {
        Application.Quit();
    }
   
}
