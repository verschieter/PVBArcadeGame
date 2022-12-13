using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float ScrollSpeed;
    Vector2 Movement;
    public Transform endPoint;

    void Update()
    {
        if (!GameManager.IsPaused)
        {
            //move the gameObject upwards untill it is or is higher then endpoint Y position
            if (transform.position.y >= -endPoint.position.y)
            {
                Movement.y = ScrollSpeed * Time.deltaTime;
                transform.position = (Vector2)transform.position - Movement;
            }
        }
    }
}
