using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExplosionItem : Item
{
    CircleCollider2D explosionCollider;
    public int damage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        explosionCollider.GetComponent<CircleCollider2D>();
        explosionCollider.isTrigger = true;
        explosionCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        transform.SetParent(player.transform);
        effectStarted = true;
        EffectTimer.StartTimer(effectDuration);
        explosionCollider.enabled = true;
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
