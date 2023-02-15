using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermantUpgradeMenu : MonoBehaviour
{
    public GameObject menuObject;


    public Bullet upgradedBullet;
    public Laser Upgradedlaser;
    public float speedMulitplier = 1.5f;
    public int extraHealth;
    public RocketLauncher launcher;

    Player player;
    List<Button> buttons;
    int buttonIndex;
    bool movedSelection;
    GameManager gameManager;


    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float verticalInput = Input.GetAxis($"P{player.id}Verti");

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
    }

    public void IncreaseMaxHealthB()
    {
        player.ChangeMaxHealth(extraHealth);
        Resume(Upgrades.HealthIncrease);
    }
    public void SpeedB()
    {
        player.moveSpeed *= speedMulitplier;
        player.fireRate /= speedMulitplier;
        player.laser.chargingTimeAmount /= speedMulitplier;
        Resume(Upgrades.SpeedUpgrade);
    }

    public void BulletUpgradeB()
    {
        player.bullet = upgradedBullet;
        Resume(Upgrades.BulletUpgrade);

    }
    public void LaserUpgradeB()
    {
        player.laser = Upgradedlaser;
        Resume(Upgrades.LaserUpgrade);

    }
    public void RocketB()
    {
        Instantiate<RocketLauncher>(launcher, player.transform).Setup(player);
        Resume(Upgrades.Rockets);
    }

    void SelectButton(int changeIndex)
    {
        buttonIndex += changeIndex;

        if (buttonIndex < 0)
            buttonIndex = buttons.Count + changeIndex;

        if (buttonIndex == buttons.Count)
            buttonIndex = 0;


        if (buttons.Count != 0)
            buttons[buttonIndex].Select();
        else
            Resume(0);
    }

    public void Spawn(Player player, List<Upgrades> disableUpgrades)
    {
        Button[] allbuttons = menuObject.GetComponentsInChildren<Button>();
        menuObject.SetActive(true);
        GameManager.IsPaused = true;
        this.player = player;
        gameManager = this.player.GiveGameManager();
        buttons = new List<Button>(allbuttons);
        for (int i = 0; i < disableUpgrades.Count; i++)
        {
            allbuttons[(int)disableUpgrades[i]].gameObject.SetActive(false);
            buttons.Remove(allbuttons[(int)disableUpgrades[i]]);
        }
        SelectButton(0);

        switch (player.id)
        {
            case 1:
                transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(25, 0);
                
                break;
            case 2:
                transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(1275, 0);
                break;


            default:
                break;
        }

    }
    void Resume(Upgrades upgrade)
    {
        GameManager.IsPaused = false;
        gameManager.ChosenUpgrade(upgrade);
        Destroy(gameObject);
    }
}
