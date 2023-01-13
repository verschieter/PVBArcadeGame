using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //health and score
    public float health;
    float maxHealth;
    public int totalScore;
    int comboAmount;
    int comboChange = 5;
    public int comboMultiplier = 1;

    UiManager uiManager;
    private GameManager gameManager;
    PlayerHud hud;
    public int id = 1;

    //Movement
    public float moveSpeed;
    Vector2 movement;
    Rigidbody2D rb;
    
    //xp
    int Xp;
    float levelUpAmount = 100;
    int timesLevelUped;
    int maxLevel = 3;

    //Inputs
    public string[] actionMapString = new string[4];

    //Firing
    public Bullet bullet;
    public Laser laser;
    Laser firingLaser;
    public float fireRate;
    public Transform firePos;
    Timer fireTimer;

    //items
    public List<Item> activeItems = new List<Item>();

    //Camara
    float Height;
    float Width;
    Vector2 Screen;
    bool hasSpawned;

   

    void Start()
    {
        fireTimer = new Timer();
        fireTimer.StartTimer(fireRate);

    }

    public void Spawned(GameManager mananger, UiManager UI, int id)
    {
        this.id = id;
        uiManager = UI;
        gameManager = mananger;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Height = sprite.bounds.size.y / 2;
        Width = sprite.bounds.size.x / 2;
        hud = uiManager.SpawnHud(id, this);
        hud.ChangeHealth(health);
        hud.ChangeScore(totalScore, comboMultiplier);

        Camera cam = Camera.main;
        Screen = cam.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, cam.transform.position.z));
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        hasSpawned = true;

    }

    public GameManager GiveGameManager()
    {
        return gameManager;
    }

    void Update()
    {
        if (!GameManager.IsPaused && hasSpawned)
        {
            if (fireTimer.IsTimerPause())
                fireTimer.PauseTimer();

            Attacking();


        }
        else
        {
            if (!fireTimer.IsTimerPause())
                fireTimer.PauseTimer();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsPaused && hasSpawned)
        {
            PlayerMovement();

            CameraBounds();
        }
        else
            rb.velocity = Vector2.zero;
    }

    private void Attacking()
    {
        float fire1 = Input.GetAxisRaw(actionMapString[2]);
        float fire2 = Input.GetAxisRaw(actionMapString[3]);

        if (fire2 > 0 && !firingLaser)
        {
            Fire(TypeFire.Laser);

        }
        if (fire2 <= 0 && firingLaser)
        {
            firingLaser.SetCharge(false);
        }


        if (fire2 <= 0 && !firingLaser)
        {
            if (fire1 > 0 && fireTimer.IsDone())
            {
                Fire(TypeFire.Bullet);
                fireTimer.StartTimer(fireRate);
            }
        }

    }

    private void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw(actionMapString[0]);
        movement.y = Input.GetAxisRaw(actionMapString[1]);
        movement.Normalize();

        rb.velocity = movement * moveSpeed;
    }

    private void CameraBounds()
    {
        //keep player inside camera view
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -Screen.x + Width, Screen.x - Width), Mathf.Clamp(transform.position.y, -Screen.y + Height, Screen.y - Height));
    }
    public void AddScore(int score, int xp)
    {
        comboAmount++;
        Xp += xp;
        if (comboAmount == comboChange && comboMultiplier != 8)
        {
            comboMultiplier *= 2;
            comboAmount = 0;
        }

        if (Xp >= levelUpAmount && timesLevelUped < maxLevel)
        {
            gameManager.Upgrade(this);
            timesLevelUped++;
            levelUpAmount *= 1.5f;
            Xp = 0;
        }

        totalScore += comboMultiplier * score;
        hud.ChangeScore(totalScore, comboMultiplier);

    }
    void Fire(TypeFire type)
    {
        switch (type)
        {
            case TypeFire.Bullet:
                Bullet tempBullet = Instantiate<Bullet>(bullet, firePos.position, Quaternion.identity);
                tempBullet.SetPlayer(this);
                break;

            case TypeFire.Laser:
                firingLaser = Instantiate<Laser>(laser, firePos.position, Quaternion.identity);
                firingLaser.transform.SetParent(transform);
                //firingLaser.SetPlayer(this);
                break;

            default:
                break;
        }
    }

    public void ChangeMaxHealth(float changeAmount)
    {
        maxHealth += changeAmount;
        hud.SetHealthBarSize(changeAmount, maxHealth);
        ChangeHealth(-changeAmount);
    }

    public void ChangeHealth(float changeAmount)
    {
        health = Mathf.Clamp(health - changeAmount, 0, maxHealth);

        comboMultiplier = 1;
        comboAmount = 0;

        hud.ChangeScore(totalScore, comboMultiplier);
        hud.ChangeHealth(health);

        if (health == 0)
        {
            gameManager.GameOver(totalScore);
            Destroy(gameObject);
        }

    }

    enum TypeFire
    {
        Bullet,
        Laser
    }


}


