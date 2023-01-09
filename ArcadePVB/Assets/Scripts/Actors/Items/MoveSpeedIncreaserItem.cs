using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedIncreaserItem : Item
{
    // Start is called before the first frame update
    public int increaseMultiplier = 2;

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
            player.moveSpeed *= increaseMultiplier;
            EffectTimer.StartTimer(effectDuration);
            effectStarted = true;
        }
    }
    protected override void ResetEffect()
    {
        if (!isDuplicate)
        {
            player.moveSpeed /= increaseMultiplier;
            player.activeItems.Remove(this);
        }
        base.ResetEffect();

    }

}
