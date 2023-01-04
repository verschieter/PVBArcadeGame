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
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        fallVector = new Vector2(0, fallSpeed);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.position -= (Vector3)fallVector * Time.deltaTime;
    }

   
}
