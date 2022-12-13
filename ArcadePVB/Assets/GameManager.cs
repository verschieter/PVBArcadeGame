using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsPaused;

    UiManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
    }
    public void GameOver()
    {
        uiManager.SetGameOverMenu(true);
        IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
