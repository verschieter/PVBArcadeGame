using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsPaused;

    UiManager uiManager;
    int amountPlayer;
    public Player[] players = new Player[2];

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        Player player1 = Instantiate<Player>(players[0]);
        player1.Spawned(this, uiManager, 1);
        Player player2 = Instantiate<Player>(players[1]);
        player2.Spawned(this, uiManager, 2);
        amountPlayer = 2;
    }
    public void GameOver()
    {
        amountPlayer--;
        if (amountPlayer == 0)
        {
            uiManager.SetGameOverMenu(true);
            IsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPaused = !IsPaused;
        }
        if (IsPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
