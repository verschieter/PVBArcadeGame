using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public PlayerHud playerHud;
    public GameObject GameOverObject;
    public List<RectTransform> spawnTransforms = new List<RectTransform>();
    // Start is called before the first frame update
    void Start()
    {
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

    public void SetGameOverMenu(bool IsGameOver)
    {
        GameOverObject.SetActive(IsGameOver);
    }

    public void RetryB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
