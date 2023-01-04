using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    float maxHealth;
    public int totalScore;
    int comboAmount;
    int comboChange = 5;
    public int comboMultiplier = 1;
    public UiManager uiManager;
    public GameManager gameManager;
    int id = 1;
    PlayerHud hud;
    //Movement
    public float moveSpeed;
    Vector2 movement;
    Rigidbody2D rb;

    public string[] actionMapString = new string[4];

    //Firing
    public Bullet bullet;
    public Laser laser;
    public float fireRate;
    float fireTime;
    public Transform firePos;
    Laser firingLaser;

    //Camara
    float Height;
    float Width;
    Vector2 Screen;
    bool hasSpawned;
    void Start()
    {
       
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

    void Update()
    {
        if (!GameManager.IsPaused && hasSpawned)
        {
            Attacking();

            PlayerMovement();

            CameraBounds();
        }
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

        if (fireTime > 0)
        {
            fireTime -= Time.deltaTime;
        }

        if (fire2 <= 0 && !firingLaser)
        {
            if (fire1 > 0 && fireTime <= 0)
            {
                Fire(TypeFire.Bullet);
                fireTime = fireRate;
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
    public void AddScore(int score)
    {
        comboAmount++;
        if (comboAmount == comboChange && comboMultiplier != 8)
        {
            comboMultiplier *= 2;
            comboAmount = 0;
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
                firingLaser.SetPlayer(this);
                break;

            default:
                break;
        }
    }

    public void ChangeHealth(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        comboMultiplier = 1;
        comboAmount = 0;

        hud.ChangeScore(totalScore, comboMultiplier);
        hud.ChangeHealth(health);

        if (health == 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    enum TypeFire
    {
        Bullet,
        Laser
    }
}


