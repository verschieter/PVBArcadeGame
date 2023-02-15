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
    public GameObject gameOverObject;
    public GameObject pauseObject;

    public List<RectTransform> spawnTransforms = new List<RectTransform>();
    public TMP_InputField nameField;
    List<Button> buttons;

    bool movedSelection;
    bool hasSaved;
    
    int buttonIndex = -1;
    int nameIndex;
    int charIndex;

    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    char[] addedChar;

    // Start is called before the first frame update
    void Start()
    {
        nameField.characterLimit = 4;
        addedChar = new char[nameField.characterLimit];
    }

    private void Update()
    {
        if (buttonIndex >= 0)
        {
            float horizantalInput = Input.GetAxis($"P1Hori");
            if (nameField.IsActive())
            {
                if (nameField.isFocused)
                {
                    PlaceCharacterInInputField(horizantalInput);
                }
            }

            float verticalInput = Input.GetAxis($"P1Verti");

            if (verticalInput < 0 && movedSelection == false)
            {
                SelectButton(1);
                movedSelection = true;
            }

            else if (verticalInput > 0 && movedSelection == false)
            {
                SelectButton(-1);
                movedSelection = true;
            }

            if (Mathf.Approximately(verticalInput, 0) && Mathf.Approximately(horizantalInput, 0))
                movedSelection = false;
        }
    }

    private void PlaceCharacterInInputField(float horizantalInput)
    {
        bool submit = Input.GetButtonDown($"Submit");
        bool remove = Input.GetButtonDown($"Remove");

        if (horizantalInput < 0 && movedSelection == false)
        {
            nameIndex--;
            if (nameIndex < 0)
                nameIndex = alpha.Length - 1;

            addedChar[charIndex] = alpha[nameIndex];
            movedSelection = true;
            nameField.text = addedChar.ArrayToString();
        }

        if (horizantalInput > 0 && movedSelection == false)
        {
            nameIndex++;
            if (nameIndex == alpha.Length)
                nameIndex = 0;

            addedChar[charIndex] = alpha[nameIndex];

            movedSelection = true;
            nameField.text = addedChar.ArrayToString();
        }

        if (submit && addedChar.ArrayToString() != string.Empty)
        {
            

            if (charIndex < addedChar.Length - 1)
                charIndex++;
            else
                SelectButton(1);

            nameIndex = 0;

        }
        if (remove)
        {
            if (charIndex > -1)
            {
                addedChar[charIndex] = ' ';
                if (charIndex > 0)
                    charIndex--;

                nameField.text = addedChar.ArrayToString();
            }
        }
    }

    void SelectButton(int changeIndex)
    {
        buttonIndex += changeIndex;

        int total = buttons.Count;

        if (nameField.IsActive())
        {
            total += 1;
        }
        if (buttonIndex < 0)
            buttonIndex = total + changeIndex;

        if (buttonIndex == total)
            buttonIndex = 0;

        if (buttonIndex == buttons.Count)
        {
            nameField.Select();
            return;
        }

        buttons[buttonIndex].Select();

    }

    public PlayerHud SpawnHud(int Id, Player player)
    {
        //if id = 1 spawn top left if 2 spawn top right
        PlayerHud temp = Instantiate<PlayerHud>(playerHud, transform);
        temp.SetPlayer(player, Id);

        switch (Id)
        {
            case 1:
                temp.SetPosition(spawnTransforms[0], TextAlignmentOptions.Left);
                temp.SetDirections(Slider.Direction.LeftToRight);
                break;

            case 2:
                temp.SetPosition(spawnTransforms[1], TextAlignmentOptions.Right);
                temp.SetDirections(Slider.Direction.RightToLeft);
                break;
            default:
                break;
        }
        return temp;
    }

    public void SetPauseMenu()
    {
        pauseObject.SetActive(!pauseObject.activeSelf);
        if (pauseObject.activeSelf)
        {
            buttons = new List<Button>(pauseObject.GetComponentsInChildren<Button>());
            buttonIndex = 0;
            SelectButton(buttonIndex);
        }
        else
        {
            buttonIndex = -1;
        }

    }

    public void SetGameOverMenu(bool IsGameOver, bool hasWon)
    {

        gameOverObject.SetActive(IsGameOver);
        if (IsGameOver)
        {
            buttons = new List<Button>(gameOverObject.GetComponentsInChildren<Button>());
            buttonIndex = 0;
            SelectButton(2);
        }

        Image image = gameOverObject.GetComponent<Image>();
        TMP_Text gameOverText = gameOverObject.GetComponentInChildren<TMP_Text>();


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
        GameManager.IsPaused = false;
        GameManager.amountPlayer = gameManager.startAmount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void MainMenuB()
    {
        if (GameManager.IsPaused)
            GameManager.IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveName()
    {
        if (addedChar.ArrayToString() == string.Empty || hasSaved == true)
        {
            return;
        }

        gameManager.SaveScore(addedChar.ArrayToString());
        highscore.DisplayHighscore();
        hasSaved = true;
    }
}
