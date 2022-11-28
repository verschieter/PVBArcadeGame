using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public float maxDistance = 3f;
    float beginY;
    // Start is called before the first frame update
    void Start()
    {
        beginY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);

        if (transform.position.y - beginY > maxDistance)
            Destroy(gameObject);

       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }
}
