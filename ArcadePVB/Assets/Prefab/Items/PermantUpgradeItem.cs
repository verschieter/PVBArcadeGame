using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermantUpgradeItem : Item
{
    public GameObject PowerUpMenu;

    public Bullet UpgradedBullet;

    public Laser Upgradedlaser;

    public float speedMulitplier = 1.5f;

    public int extraHealth;

    public RocketLauncher launcher;

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

    public void IncreaseMaxHealthB()
    {
        player.ChangeMaxHealth(extraHealth);
    }
    public void SpeedB()
    {
        player.moveSpeed *= speedMulitplier;
        player.fireRate *= speedMulitplier;
        player.laser.chargingTime /= speedMulitplier;
    }

    public void BulletUpgradeB()
    {
        player.bullet = UpgradedBullet;

    }
    public void LaserUpgradeB()
    {
        player.laser = Upgradedlaser;

    }
    public void RocketB()
    {
        Instantiate<RocketLauncher>(launcher, player.transform).Setup(player);

    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();

        PowerUpMenu.SetActive(true);
        GameManager.IsPaused = true;
        if (!isDuplicate)
        {
            EffectTimer.StartTimer(effectDuration);



            effectStarted = true;
        }
    }
    protected override void ResetEffect()
    {
        if (!isDuplicate)
        {

        }
        base.ResetEffect();
    }
}
