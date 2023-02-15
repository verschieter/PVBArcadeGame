using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Astroide : Enemy
{
    // Start is called before the first frame update

    public Vector3 fallSpeed;
    public ParticleSystem explosionParticle;
    public ParticleSystem trailParticle;
    public float damage;
    bool isDestroyed = false;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (rb.velocity == Vector2.zero)
                rb.velocity = -fallSpeed;

            if (explosionParticle.isStopped && isDestroyed == true)
            {
                if (spawnManager)
                    spawnManager.RemoveFromList(this);

                Destroy(gameObject);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.ChangeHealth(damage);
            source.clip = sounds;
            source.Play();
            explosionParticle.Play();
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            return;
        }
        trailParticle.Stop();
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        isDestroyed = true;
    }
}
