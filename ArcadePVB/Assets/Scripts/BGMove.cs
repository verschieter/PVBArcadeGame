using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float ScrollSpeed;
    Vector2 Movement;
    public Transform endPoint;
    public GameManager gameManager;

    void Update()
    {
        if (!GameManager.IsPaused)
        {
            MovingBackGround();
        }
    }

    private void MovingBackGround()
    {
        //move the gameObject upwards untill it is or is higher then endpoint Y position
        if (transform.position.y >= -endPoint.position.y)
        {
            Movement.y = ScrollSpeed * Time.deltaTime;
            transform.position = (Vector2)transform.position - Movement;
        }
        else
        {
            gameManager.GameWon();
        }
    }

    public float DistanceToFinish()
    {
        return Vector2.Distance(transform.position, -endPoint.position);
    }
}
