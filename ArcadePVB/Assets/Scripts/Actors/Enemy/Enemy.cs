using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public float health;
    public int scorePoint;
    protected Rigidbody2D rb;
    [SerializeField]
    protected Item item;
    protected SpawnManager spawnManager;
    int maxChangeOfItem = 10;
    public int xpAmount;

    protected AudioSource source;
    public AudioClip sounds;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        rb.gravityScale = 0;
    }

    public void TakeDamage(float damage, Player player)
    {
        health = Mathf.Clamp(health - damage, 0, Mathf.Infinity);
        if (health == 0)
            Destroyed(player);
    }

    public void SetItem(Item item, SpawnManager spawn)
    {
        int random = Random.Range(0, maxChangeOfItem);

        if (random == 5)
        {
            this.item = item;
        }
        spawnManager = spawn;
    }

    public void Destroyed(Player player)
    {
        if (item)
            Instantiate<Item>(item, transform.position, Quaternion.identity);
        if (player)
            player.AddScore(scorePoint, xpAmount);
        
        if (spawnManager)
            spawnManager.RemoveFromList(this);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
