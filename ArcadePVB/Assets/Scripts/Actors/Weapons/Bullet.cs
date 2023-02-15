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
        base.Update();
        if (!GameManager.IsPaused)
        {

            if (impact.isStopped)
                transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }

    public override void HitBlock()
    {
        base.HitBlock();
    }
    public override void OnEnemyCollision(GameObject enemyObject)
    {
        base.OnEnemyCollision(enemyObject);
        impact.Play();
    }
}
