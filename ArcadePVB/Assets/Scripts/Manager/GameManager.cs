using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsPaused;

    public UiManager uiManager;
    public static int amountPlayer = 1;
    public int startAmount;
    public Player[] players = new Player[2];
    public SaveManager saveManager;
    public SpawnManager spawnManager;
    int totalScore;
    Vector2 spawnOffset = new Vector2(-0.3f, 0);

    public PermantUpgradeMenu UpgradeMenu;
    List<Upgrades> upgrades = new List<Upgrades>();
    bool gameEnded;

    Timer pauseTimer;
    float pauseTimeAmount = 0.5f;

    Player player1;
    Player player2;
    // Start is called before the first frame update
    void Start()
    {
        IsPaused = false;
        startAmount = amountPlayer;
        pauseTimer = new Timer();
        pauseTimer.StartTimer(pauseTimeAmount);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 1; amountPlayer >= i; i++)
        {
            switch (i)
            {
                case 1:
                    player1 = Instantiate<Player>(players[0], spawnOffset, players[0].transform.rotation);
                    player1.Spawned(this, uiManager, 1);

                    break;
                case 2:
                    player2 = Instantiate<Player>(players[1], -spawnOffset, players[1].transform.rotation);
                    player2.Spawned(this, uiManager, 2);
                    break;
            }
        }
    }
    public void GameOver(int score)
    {
        amountPlayer--;
        totalScore += score;
        if (amountPlayer == 0)
        {
            gameEnded = true;
            ShowGameOverScreen(false);
        }
    }
    public void GameWon()
    {
        spawnManager.GameOver();
    }

    public void ShowGameOverScreen(bool hasWon)
    {
        if (!IsPaused)
        {
            if (hasWon)
            {

                if (amountPlayer == 1)
                {
                    if (player1)
                        totalScore += player1.totalScore;

                    else
                        totalScore += player2.totalScore;

                }

                else
                    totalScore = player1.totalScore + player2.totalScore;
            }

            uiManager.SetGameOverMenu(true, hasWon);
            IsPaused = true;
        }
    }

    // Update is called once per frame

    public void SaveScore(string name)
    {
        saveManager.IsInHighscore(name, totalScore);
    }

    public void Upgrade(Player player)
    {
        Instantiate<PermantUpgradeMenu>(UpgradeMenu).Spawn(player, upgrades);

    }

    public void ChosenUpgrade(Upgrades type)
    {
        upgrades.Add(type);
    }

    void Pause()
    {
        if (!IsPaused || uiManager.pauseObject.activeSelf)
        {
            IsPaused = !IsPaused;
            uiManager.SetPauseMenu();
            pauseTimer.StartTimer(pauseTimeAmount);
        }

    }

    void Update()
    {

        if (Input.GetAxis("Pause") > 0 && pauseTimer.IsDone() && !gameEnded)
        {
            Pause();
        }

        if (spawnManager.AllEnmiesDied())
        {
            gameEnded = true;
            ShowGameOverScreen(true);
        }
    }
}


public enum Upgrades
{
    BulletUpgrade,
    LaserUpgrade,
    Rockets,
    SpeedUpgrade,
    HealthIncrease,
}