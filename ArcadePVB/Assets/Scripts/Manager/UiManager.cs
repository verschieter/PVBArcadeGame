using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UiManager : MonoBehaviour
{
    public PlayerHud playerHud;
    public GameManager gameManager;
    public Highscore highscore;
    public GameObject GameOverObject;
    public GameObject PauseObject;

    public List<RectTransform> spawnTransforms = new List<RectTransform>();
    public TMP_InputField nameField;

    bool hasSaved;

    // Start is called before the first frame update
    void Start()
    {
        nameField.characterLimit = 4;

    }

    public PlayerHud SpawnHud(int Id, Player player)
    {
        //if id = 1 spawn top left if 2 spawn top right
        PlayerHud temp = Instantiate<PlayerHud>(playerHud, transform);
        temp.SetPlayer(player, Id);

        switch (Id)
        {
            case 1:
                temp.SetPosition(spawnTransforms[0]);
                temp.SetDirections(Slider.Direction.LeftToRight);
                break;

            case 2:
                temp.SetPosition(spawnTransforms[1]);
                temp.SetDirections(Slider.Direction.RightToLeft);
                break;
            default:
                break;
        }
        return temp;
    }

    public void SetPauseMenu()
    {
        PauseObject.SetActive(!PauseObject.activeSelf);
    }

    public void SetGameOverMenu(bool IsGameOver, bool hasWon)
    {
        GameOverObject.SetActive(IsGameOver);
        Image image = GameOverObject.GetComponent<Image>();
        TMP_Text gameOverText = GameOverObject.GetComponentInChildren<TMP_Text>();


        if (hasWon)
        {
            image.color = new Color(0, 0.60f, 0, 0.5f);
            gameOverText.text = "Game Won";

        }
        else
        {
            image.color = new Color(0.60f, 0, 0, 0.5f);
            gameOverText.text = "Game Over";

        }



    }

    public void RetryB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.IsPaused = false;
    }
    public void MainMenuB()
    {
        if (GameManager.IsPaused)
            GameManager.IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveName()
    {
        if (nameField.text == string.Empty || hasSaved == true)
        {
            return;
        }

        gameManager.SaveScore(nameField.text);
        highscore.DisplayHighscore();
        hasSaved = true;
    }

    
  
}
