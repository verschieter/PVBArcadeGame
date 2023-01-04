using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyShip : Enemy
{
    Transform[] wayPoints;
    Transform moveToPoint;
    int wavepointIndex;
    public float speed = 0.5f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();


    }

    public void SetWayPoints(Transform parentPoint)
    {
        wayPoints = new Transform[parentPoint.childCount];


        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = parentPoint.GetChild(i);
        }

        moveToPoint = wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = moveToPoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //transform.LookAt(moveToPoint);

        if (Vector3.Distance(transform.position, moveToPoint.position) <= 0.4f)
        {
            GetNextWaypoint();
        }


    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        moveToPoint = wayPoints[wavepointIndex];
    }
}
