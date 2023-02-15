using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float scrollSpeed;
    Vector2 movement;
    public Transform endPoint;
    public GameManager gameManager;

    void Update()
    {
        if (!GameManager.IsPaused)
        {
            MovingBackground();
        }
    }

    private void MovingBackground()
    {
        //move the gameObject upwards untill it is or is higher then endpoint Y position
        if (transform.position.y >= -endPoint.position.y)
        {
            movement.y = scrollSpeed * Time.deltaTime;
            transform.position = (Vector2)transform.position - movement;
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
