using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroide : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 fallSpeed;
    public ParticleSystem explosionParticle;
    public ParticleSystem trailParticle;
    public float damage;
    public int scorePoint;
    Rigidbody2D rb;
    bool isDestroyed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (rb.velocity == Vector2.zero)
                rb.velocity = -fallSpeed;


            if (explosionParticle.isStopped && isDestroyed == true)
                Destroy(gameObject);
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
            player.TakeDamage(damage);
            //TODO: explosion
            explosionParticle.Play();

        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            return;
        }
        trailParticle.Stop();
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        isDestroyed = true;
    }
}
