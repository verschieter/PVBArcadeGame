using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public Slider healthBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    Player player;
    RectTransform rect = new RectTransform();
    string startText;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        startText = scoreText.text;
    }
    // Start is called before the first frame update

    
    public void SetDirections(Slider.Direction dir)
    {
        healthBar.direction = dir;

    }

    public void SetPosition(RectTransform position)
    {
        rect.pivot = position.pivot;
        rect.anchoredPosition = position.anchoredPosition;
        rect.anchorMin = position.anchorMin;
        rect.anchorMax = position.anchorMax;
    }

    public void SetPlayer(Player player, int id)
    {
        this.player = player;
        playerText.text += id.ToString();
    }

    public void ChangeHealth(float newHealth)
    {
        healthBar.value = player.health;
    }
    public void ChangeScore(int newTotalScore, int newCombo)
    {
        scoreText.text = startText + player.totalScore.ToString();
        comboText.text = "x " + player.comboMultiplier.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
