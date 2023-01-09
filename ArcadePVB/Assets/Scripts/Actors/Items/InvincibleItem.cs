using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : Item
{
    SpriteRenderer playerSprite;
    float newAlpha = 0.5f;
    float oldAlpha = 1f;
    Color color;
    int orderChange = 0;
    int previousOrder = 2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        if (!isDuplicate)
        {
            EffectTimer.StartTimer(effectDuration);

            player.gameObject.layer = LayerMask.NameToLayer("OnlyItems");
            playerSprite = player.GetComponent<SpriteRenderer>();
            color = playerSprite.color;
            color.a = newAlpha;
            playerSprite.color = color;
            playerSprite.sortingOrder = orderChange;

            effectStarted = true;
        }
    }
    protected override void ResetEffect()
    {
        if (!isDuplicate)
        {

            playerSprite.sortingOrder = previousOrder;
            color.a = oldAlpha;
            playerSprite.color = color;
            player.gameObject.layer = LayerMask.NameToLayer("Player");
            player.activeItems.Remove(this);
        }
        base.ResetEffect();
    }
}
