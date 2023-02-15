using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(ParticleSystem)), RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public float damage;
    protected ParticleSystem impact;
    protected Player player;
    protected AudioSource source;
    public List<AudioClip> sounds = new List<AudioClip>();
    bool hit;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        impact = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        source.clip = sounds[0];
        source.Play();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (impact.isStopped && hit)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public virtual void HitBlock()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("BlockB"))
        {
            HitBlock();
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnEnemyCollision(col.gameObject);

            return;
        }

    }
    public virtual void OnEnemyCollision(GameObject enemyObject)
    {
        if (enemyObject.layer == LayerMask.NameToLayer("Astroide"))
        {
            Astroide astroide = enemyObject.GetComponent<Astroide>();
            astroide.TakeDamage(damage, player);
        }
        else if (enemyObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyShip ship = enemyObject.GetComponent<EnemyShip>();
            ship.TakeDamage(damage, player);

        }
        hit = true;
      
    }

}
