using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Weapon
{
    public float moveSpeed;
    public float maxDistance = 3f;
    float beginY;

    CircleCollider2D circle;
    BoxCollider2D box;
    SpriteRenderer sprite;

    bool isExploding;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        circle.enabled = false;
    }
    public void Setup(Player player)
    {
        this.player = player;
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!GameManager.IsPaused)
        {
            if (!isExploding)
            {
                transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }

            if (isExploding && impact.isStopped)
            {
                Destroy(gameObject);
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Astroide") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            box.enabled = false;
            circle.enabled = true;
            sprite.enabled = false;
            isExploding = true;
            impact.Play();
            return;
        }

        Destroy(gameObject);
    }
    public override void HitBlock()
    {
        return;
    }
}
