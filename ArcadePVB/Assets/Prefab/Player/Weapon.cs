using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(ParticleSystem))]
public class Weapon : MonoBehaviour
{
    public float damage;
    protected ParticleSystem Impact;
    protected Player player;
    protected Enemy enemy;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Impact = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (Impact.isStopped && enemy)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.layer == LayerMask.NameToLayer("Astroide") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        OnEnemyCollision(col.gameObject);
    //        Impact.Play();
    //        return;
    //    }

    //    Destroy(gameObject);

    //}

    private void OnTriggerEnter2D(Collider2D col)
    {

        //if (col.gameObject.layer == LayerMask.NameToLayer("BlockB"))
        //{
        //    HitBlock(col.gameObject);
        //}

        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnEnemyCollision(col.gameObject);
            return;
        }

    }
    //public virtual void HitBlock(GameObject blockObject)
    //{
    //    Destroy(gameObject);
    //}

    public virtual void OnEnemyCollision(GameObject enemyObject)
    {
        if (enemyObject.layer == LayerMask.NameToLayer("Astroide"))
        {
            Astroide astroide = enemyObject.GetComponent<Astroide>();
            enemy = enemyObject.GetComponent<Enemy>();
            astroide.TakeDamage(damage, player);
        }
        else if (enemyObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyShip ship = enemyObject.GetComponent<EnemyShip>();
            ship.TakeDamage(damage, player);
            enemy = enemyObject.GetComponent<Enemy>();

        }
        Impact.Play();
    }

}
