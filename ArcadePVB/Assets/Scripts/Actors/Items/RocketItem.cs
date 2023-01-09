using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketItem : Item
{
    public float fireRate;

    Timer FireTimer;
    Transform firePos;
    public Rocket rocket;

    int rocketAmount = 2;

    Vector3 offset = new Vector2(0.15f, 0.15f);

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        FireTimer = new Timer();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (FireTimer.IsDone())
        {
            Fire();
            FireTimer.StartTimer(fireRate);
        }
    }

    void Fire()
    {
        for (int i = 0; i < rocketAmount; i++)
        {
            Rocket tempRocket = Instantiate<Rocket>(rocket, firePos.position - offset, rocket.transform.rotation);
            offset.x = -offset.x;
            tempRocket.Setup(player);
        }
    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        if (!isDuplicate)
        {
            effectStarted = true;
            EffectTimer.StartTimer(effectDuration);
            firePos = player.firePos;
            FireTimer.StartTimer(fireRate);
        }
    }

    protected override void ResetEffect()
    {
       
        base.ResetEffect();

    }
}
