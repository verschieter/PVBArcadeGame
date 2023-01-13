using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExplosionItem : Item
{
    CircleCollider2D explosionCollider;
    public int damage;
    public ParticleSystem explosion;
    public AudioClip clip;
    AudioSource source;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
        explosionCollider = GetComponent<CircleCollider2D>();
        explosionCollider.isTrigger = true;
        explosionCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(effectStarted && explosion.isStopped)
        {
            player.activeItems.Remove(this);
            ResetEffect();
        }
    }

    public override void OnPlayerCollision()
    {
        source.clip = clip;
        source.Play();
        base.OnPlayerCollision();
        explosion.Play();
        effectStarted = true;
        EffectTimer.StartTimer(effectDuration);
        explosionCollider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Bullet");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide"))
        {
            Astroide astroide = col.gameObject.GetComponent<Astroide>();
            astroide.TakeDamage(damage, player);

            return;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyShip ship = col.gameObject.GetComponent<EnemyShip>();
            ship.TakeDamage(damage, player);
            return;
        }
    }

    protected override void ResetEffect()
    {
        base.ResetEffect();
    }
}
