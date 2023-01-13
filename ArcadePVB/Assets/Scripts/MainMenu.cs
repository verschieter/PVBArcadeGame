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
    List<Button> buttons;
    bool movedSelection;
    int index;
    bool hasSwitched;
    bool IsPlay;
    Menu menuB;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        buttons = new List<Button>(GetComponentsInChildren<Button>());
    }

    public void PlayerSelectB()
    {
        IsPlay = !PlayerSelectMenu.activeSelf;
        PlayerSelectMenu.SetActive(!PlayerSelectMenu.activeSelf);
        hasSwitched = true;

        if (PlayerSelectMenu.activeSelf)
            menuB = Menu.SelectPlayer;
        else
            menuB = Menu.Main;


    }


    public void Play(int amountOfPlayer)
    {
        GameManager.amountPlayer = amountOfPlayer;
        SceneManager.LoadScene("Level1");

    }


    public void CreditsB()
    {
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
        hasSwitched = true;
        if (CreditsMenu.activeSelf)
            menuB = Menu.Credits;
        else
            menuB = Menu.Main;
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {

        switch (menuB)
        {
            case Menu.Main:
                MoveThroughMenu();
                break;
            case Menu.SelectPlayer:
                SelectedAmountPlayer();
                break;
            case Menu.Credits:
                if (hasSwitched)
                {
                    buttons.Clear();
                    buttons = new List<Button>(CreditsMenu.GetComponentsInChildren<Button>());
                    hasSwitched = false;
                    SelectButton(0);
                }
                break;
        }

    }

    private void MoveThroughMenu()
    {
        if (hasSwitched)
        {
            buttons.Clear();
            buttons = new List<Button>(GetComponentsInChildren<Button>());
            hasSwitched = false;
            SelectButton(0);
        }

        float verticalInput = Input.GetAxis($"P1Verti");

        if (verticalInput < 0 && movedSelection == false)
        {
            SelectButton(1);
            movedSelection = true;
        }

        if (verticalInput > 0 && movedSelection == false)
        {
            SelectButton(-1);
            movedSelection = true;
        }

        if (Mathf.Approximately(verticalInput, 0))
            movedSelection = false;
    }

    void SelectedAmountPlayer()
    {
        if (hasSwitched)
        {
            buttons.Clear();
            buttons = new List<Button>(PlayerSelectMenu.GetComponentsInChildren<Button>());
            hasSwitched = false;
            SelectButton(0);

        }
        float horizantalInput = Input.GetAxis($"P1Hori");

        if (horizantalInput < 0 && movedSelection == false)
        {
            SelectButton(-1);
            movedSelection = true;
        }

        if (horizantalInput > 0 && movedSelection == false)
        {
            SelectButton(1);
            movedSelection = true;
        }
        if (Mathf.Approximately(horizantalInput, 0))
            movedSelection = false;
    }




    void SelectButton(int changeIndex)
    {
        index += changeIndex;

        if (index < 0)
            index = buttons.Count + changeIndex;

        if (index == buttons.Count)
            index = 0;


        buttons[index].Select();

    }
    enum Menu
    {
        Main,
        SelectPlayer,
        Credits
    }
}
