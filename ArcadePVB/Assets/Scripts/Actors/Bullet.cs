using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public float maxDistance = 3f;
    float beginY;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        beginY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {

            transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);


        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide"))
        {
            Astroide astroide = col.gameObject.GetComponent<Astroide>();
            astroide.TakeDamage(damage);
            player.AddScore(astroide.scorePoint);
            return;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyShip ship = col.gameObject.GetComponent<EnemyShip>();
            ship.TakeDamage(damage);
            player.AddScore(ship.scorePoint);
            return;
        }

        
        if (col.gameObject.layer != LayerMask.NameToLayer("Block"))
            Destroy(col.gameObject);

        Destroy(gameObject);
        
    }
}
