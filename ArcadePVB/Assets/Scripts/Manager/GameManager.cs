using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsPaused;

    public UiManager uiManager;
    public static int amountPlayer = 1;
    public Player[] players = new Player[2];
    public SaveManager saveManager;
    public SpawnManager spawnManager;
    int totalscore;
    Vector2 spawnOffset = new Vector2(-0.3f, 0);
    

    bool gameEnded;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; amountPlayer >= i; i++)
        {
            switch (i)
            {
                case 1:
                    Player player1 = Instantiate<Player>(players[0], spawnOffset, players[0].transform.rotation);
                    player1.Spawned(this, uiManager, 1);

                    break;
                case 2:
                    Player player2 = Instantiate<Player>(players[1], -spawnOffset, players[1].transform.rotation);
                    player2.Spawned(this, uiManager, 2);
                    break;
            }

        }

    }
    public void GameOver(int score)
    {
        amountPlayer--;
        totalscore += score;
        if (amountPlayer == 0)
        {
            ShowGameOverScreen(false);
        }
    }
    public void GameWon()
    {
        totalscore = 0;

        if (amountPlayer == 1)
            totalscore = players[0].totalScore;

        else
            totalscore = players[0].totalScore + players[1].totalScore;

        gameEnded = true;
        spawnManager.GameOver();
    }

    public void ShowGameOverScreen(bool hasWon)
    {

        uiManager.SetGameOverMenu(true, hasWon);
        IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    // Update is called once per frame

    public void SaveScore(string name)
    {
        saveManager.IsInHighscore(name, totalscore);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPaused = !IsPaused;
            uiManager.SetPauseMenu();
        }

        if (gameEnded && spawnManager.AllEnmiesDied())
        {
            ShowGameOverScreen(true);
        }
    }
}
