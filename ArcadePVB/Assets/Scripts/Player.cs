using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    Vector2 movement;
    Rigidbody2D rb;

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

    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Height = sprite.bounds.size.y / 3;
        Width = sprite.bounds.size.x / 3;
        Camera cam = Camera.main;
        Screen = cam.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, cam.transform.position.z));
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Attacking();

        PlayerMovement();

        CameraBounds();
    }

    private void Attacking()
    {
        float fire1 = Input.GetAxisRaw("Fire1");
        float fire2 = Input.GetAxisRaw("Fire2");
        if (fire1 > 0 && fireTime <= 0)
        {
            Fire("bullet");
            fireTime = fireRate;
        }

        if(fire2 > 0 && !firingLaser)
        {
            firingLaser = Instantiate<Laser>(laser,firePos.position, Quaternion.identity);
            firingLaser.transform.SetParent(transform);
        }
        if(fire2 <= 0 && firingLaser)
        {
            firingLaser.UnleachCharge();
            firingLaser = null;
        }

        if (fireTime > 0)
        {
            fireTime -= Time.deltaTime;
        }
    }

    private void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        rb.velocity = movement * moveSpeed;
    }

    private void CameraBounds()
    {
        //keep player inside camera view
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -Screen.x + Width, Screen.x - Width), Mathf.Clamp(transform.position.y, -Screen.y + Height, Screen.y - Height));
    }

    void Fire(string TypeFire)
    {
        Instantiate<Bullet>(bullet, firePos.position, Quaternion.identity);
    }
}


