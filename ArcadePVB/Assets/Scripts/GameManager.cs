using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsPaused;

    UiManager uiManager;
    public int amountPlayer = 1;
    public Player[] players = new Player[2];

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        for (int i = 1; amountPlayer >= i; i++)
        {
            switch (i)
            {
                case 1:
                    Player player1 = Instantiate<Player>(players[0]);
                    player1.Spawned(this, uiManager, 1);

                    break;
                case 2:
                    Player player2 = Instantiate<Player>(players[1]);
                    player2.Spawned(this, uiManager, 2);
                    break;
            }

        }

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
