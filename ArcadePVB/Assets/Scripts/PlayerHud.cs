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
    RectTransform rectTransform = new RectTransform();
    string startText;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        startText = scoreText.text;
    }
    // Start is called before the first frame update

    public void SetHealthBarSize(float WidthAmount, float newMaxHealth)
    {
        float newWidth = healthBar.GetComponent<RectTransform>().rect.width + (WidthAmount * 4);
        healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        healthBar.maxValue = newMaxHealth;
        // set the fill area size correctly to not overlap when extended
        healthBar.transform.GetChild(1).GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 95, 500);
    }
    public void SetDirections(Slider.Direction dir)
    {
        healthBar.direction = dir;

    }

    public void SetPosition(RectTransform position, TextAlignmentOptions alignment)
    {
        rectTransform.pivot = position.pivot;
        rectTransform.anchoredPosition = position.anchoredPosition;
        rectTransform.anchorMin = position.anchorMin;
        rectTransform.anchorMax = position.anchorMax;
        comboText.alignment = alignment;
    }

    public void SetPlayer(Player player, int id)
    {
        this.player = player;
        playerText.text += id.ToString();
    }

    public void ChangeHealth()
    {
        healthBar.value = player.health;
    }
    public void ChangeScore()
    {
        scoreText.text = startText + player.totalScore.ToString();
        comboText.text = "x " + player.comboMultiplier.ToString();
    }

}
