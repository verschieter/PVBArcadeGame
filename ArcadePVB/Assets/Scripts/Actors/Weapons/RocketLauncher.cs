using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    Player player;

    public float fireRate;

    Timer FireTimer;
    public Transform firePos;
    public Rocket rocket;
    int rocketAmount = 2;

    Vector3 offset = new Vector2(0.15f, 0.15f);
    // Start is called before the first frame update

    public void Setup(Player player)
    {
        this.player = player;
        firePos = player.firePos;
        FireTimer = new Timer();
        FireTimer.StartTimer(fireRate * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (FireTimer.IsTimerPause())
                FireTimer.PauseTimer();

            if (FireTimer.IsDone())
            {
                Fire();
                FireTimer.StartTimer(fireRate);
            }
        }
        else
        {
            if (!FireTimer.IsTimerPause())
                FireTimer.PauseTimer();
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
}
