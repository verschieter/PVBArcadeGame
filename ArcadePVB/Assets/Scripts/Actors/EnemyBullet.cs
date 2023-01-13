using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float damage;
    Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
            rb.velocity = movement;
        else
            rb.velocity = Vector2.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.ChangeHealth(damage);
        }

        Destroy(gameObject);
    }
}
