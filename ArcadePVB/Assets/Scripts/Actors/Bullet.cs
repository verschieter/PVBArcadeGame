using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    public float moveSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!GameManager.IsPaused)
        {
            base.Update();

            transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);


        }
    }


    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.layer == LayerMask.NameToLayer("Astroide"))
    //    {
    //        Astroide astroide = col.gameObject.GetComponent<Astroide>();
    //        astroide.TakeDamage(damage, player);
    //    }
    //    else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        EnemyShip ship = col.gameObject.GetComponent<EnemyShip>();
    //        ship.TakeDamage(damage, player);
    //    }
    //    Destroy(gameObject);

    //}
    //private void OnCollisionEnter2D(Collision2D col)
    //{



    //    Destroy(gameObject);

    //}
}
