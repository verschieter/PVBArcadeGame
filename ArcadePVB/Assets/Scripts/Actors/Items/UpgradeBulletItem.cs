using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletItem : Item
{
    public Bullet UpgradedBullet;
    Bullet previousB;

    public Laser Upgradedlaser;
    Laser previousL;
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
            previousB = player.bullet;
            player.bullet = UpgradedBullet;
            previousL = player.laser;
            player.laser = Upgradedlaser;



            EffectTimer.StartTimer(effectDuration);
            effectStarted = true;
        }

    }
    protected override void ResetEffect()
    {
        if (!isDuplicate)
        {

            player.bullet = previousB;
            player.laser = previousL;
            player.activeItems.Remove(this);
        }
        base.ResetEffect();

    }
}
