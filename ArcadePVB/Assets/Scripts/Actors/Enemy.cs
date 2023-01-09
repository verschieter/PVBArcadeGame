using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float health;
    public int scorePoint;
    protected Rigidbody2D rb;
    protected Item item;
    protected SpawnManager spawnManager;
    int maxChangeOfItem = 10;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, Mathf.Infinity);

        if (health == 0)
            Destroyed();
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

    public void Destroyed()
    {
        if (item)
            Instantiate<Item>(item, transform.position, Quaternion.identity);

        spawnManager.RemoveFromList(this);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
