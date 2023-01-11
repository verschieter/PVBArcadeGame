using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public float maxDistance = 3f;
    float beginY;

    CircleCollider2D circle;
    BoxCollider2D box;
    SpriteRenderer sprite;

    ParticleSystem particle;
    bool isExploding;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();

        circle.enabled = false;
    }
    public void Setup(Player player)
    {
        this.player = player;
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (!isExploding)
            {
                transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }

            if (isExploding && particle.isStopped)
            {
                Destroy(gameObject);
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            box.enabled = false;
            circle.enabled = true;
            sprite.enabled = false;
            isExploding = true;
            particle.Play();
            return;
        }

        if (col.gameObject.layer != LayerMask.NameToLayer("BlockB"))
            Destroy(col.gameObject);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide"))
        {
            Astroide astroide = col.gameObject.GetComponent<Astroide>();
            astroide.TakeDamage(damage, player);
            circle.enabled = true;
            return;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyShip ship = col.gameObject.GetComponent<EnemyShip>();
            ship.TakeDamage(damage, player);
            return;
        }
    }
}
