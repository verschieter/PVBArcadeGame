using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyShip : Enemy
{
    Transform[] wayPoints;
    Transform moveToPoint;
    public EnemyBullet bullet;
    int wavepointIndex;
    public float speed = 0.5f;
    public float fireRate = 1f;
    Timer fireTimer;
    // Start is called before the first frame update

    public EnemyShip()
    {
        fireTimer = new Timer();
    }
    public override void Start()
    {
        base.Start();

        fireTimer.StartTimer(1, 3);
    }

    public void SetWayPoints(Transform parentPoint)
    {
        wayPoints = new Transform[parentPoint.childCount];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = parentPoint.GetChild(i);
        }
        transform.position = wayPoints[0].position;
        moveToPoint = wayPoints[1];
    }

    // Update is called once per frame
     void Update()
    {
        if (!GameManager.IsPaused)
        {
            Vector3 dir = moveToPoint.position - transform.position;

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            //transform.LookAt(moveToPoint);

            if (Vector3.Distance(transform.position, moveToPoint.position) <= 0.4f)
            {
                GetNextWaypoint();
            }

            if (fireTimer.IsDone())
            {
                Fire();
                fireTimer.StartTimer(fireRate);
            }
        }
    }

    void Fire()
    {
        Instantiate<EnemyBullet>(bullet, transform.position, Quaternion.identity);
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
