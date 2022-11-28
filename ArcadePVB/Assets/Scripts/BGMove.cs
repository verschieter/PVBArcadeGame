using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float ScrollSpeed;
    Vector2 Movement;
    public Transform endPoint;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= -endPoint.position.y)
        {
            Movement.y = ScrollSpeed * Time.deltaTime;
            transform.position = (Vector2)transform.position - Movement;
        }
    }
}
