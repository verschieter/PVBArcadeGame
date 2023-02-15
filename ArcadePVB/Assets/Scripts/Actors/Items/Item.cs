using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    protected Player player;
    Vector2 fallVector;
    Rigidbody2D rb;
    float fallSpeed = 0.5f;

    protected Timer effectTimer;
    protected bool effectStarted;
    [SerializeField]
    protected float effectDuration = 1;
    protected BoxCollider2D box;
    protected SpriteRenderer sprite;
    protected bool isDuplicate;
    protected Item duplicateItem;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        effectTimer = new Timer();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        fallVector = new Vector2(0, fallSpeed);
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

    }

    protected virtual void ResetEffect()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.IsPaused)
        {
            if (effectTimer.IsTimerPause())
                effectTimer.PauseTimer();

            if (!effectStarted)
                transform.position -= (Vector3)fallVector * Time.deltaTime;

            if (effectStarted && effectTimer.IsDone())
            {
                ResetEffect();
            }
        }
        else
        {
            if (!effectTimer.IsTimerPause())
                effectTimer.PauseTimer();
        }
    }

    public void AddToTimer(float addedTime)
    {
        effectTimer.StartTimer(effectTimer.RemainingTime() + addedTime);
    }

    public virtual void OnPlayerCollision()
    {
        box.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < player.activeItems.Count; i++)
        {

            if (player.activeItems[i].GetType() == this.GetType() && player.activeItems[i] != this)
            {
                isDuplicate = true;
                duplicateItem = player.activeItems[i];
            }

        }
        if (isDuplicate)
        {
            duplicateItem.AddToTimer(effectDuration);
            ResetEffect();
        }
        else
        {
            player.activeItems.Add(this);
        }
    }


    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") || col.gameObject.layer == LayerMask.NameToLayer("OnlyItems"))
        {
            player = col.gameObject.GetComponent<Player>();
            OnPlayerCollision();
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("BlockA"))
            Destroy(gameObject);
    }

}
