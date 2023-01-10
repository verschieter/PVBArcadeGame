using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{

    public Astroide astroide;
    public EnemyShip ship;
    public BGMove bg;
    public List<Transform> waypointsParents = new List<Transform>();

    Timer astroideTimer;
    Timer enemyTimer;

    float waveLenght = 5;
    bool isDoneSpawning = true;

    public List<Item> allItems = new List<Item>();

    List<Enemy> emeniesAlive = new List<Enemy>();

    bool gameEnded;
    float totalDistance;
    float spawnDistance;
    float spawnDivider = 30;
    int wayPointIndex;

    float timePassed;
    public SpawnManager()
    {
        astroideTimer = new Timer();
        enemyTimer = new Timer();
    }
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = bg.DistanceToFinish();

        spawnDistance = totalDistance / spawnDivider;

        astroideTimer.StartTimer(0.8f, 4.6f);
        enemyTimer.StartTimer(0.8f, 4.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused && gameEnded == false)
        {
            if (enemyTimer.IsTimerPause())
                enemyTimer.PauseTimer();

            if (astroideTimer.IsTimerPause())
                astroideTimer.PauseTimer();


            SpawnAstroide();
            float currentDistance = bg.DistanceToFinish();
            if (currentDistance <= totalDistance - spawnDistance)
            {
                SpawmnEnemy();
                spawnDistance += totalDistance / spawnDivider;
            }
        }
        else
        {
            timePassed += Time.deltaTime;
            if (!enemyTimer.IsTimerPause())
                enemyTimer.PauseTimer();

            if (!astroideTimer.IsTimerPause())
                astroideTimer.PauseTimer();

        }
    }

    public void GameOver()
    {
        gameEnded = true;
    }
    private void SpawnAstroide()
    {

        if (astroideTimer.IsDone())
        {
            Vector2 spawnPos = new Vector2(Random.Range(-2.69f, 2.69f), transform.position.y);
            Astroide tempAstroide = Instantiate<Astroide>(astroide, spawnPos, Quaternion.identity);


            int random = Random.Range(0, allItems.Count);
            tempAstroide.SetItem(allItems[random], this);

            astroideTimer.StartTimer(2f, 4.6f);
            emeniesAlive.Add(tempAstroide);
        }
    }

    private void SpawmnEnemy()
    {
        if (isDoneSpawning)
        {
            isDoneSpawning = false;
            StartCoroutine(SpawnShip());
        }
    }

    public bool AllEnmiesDied()
    {
        return emeniesAlive.Count == 0;
    }

    public void RemoveFromList(Enemy enemy)
    {
        emeniesAlive.Remove(enemy);
    }

    IEnumerator SpawnShip()
    {
        if (wayPointIndex == waypointsParents.Count)
            wayPointIndex = 0;
        for (int i = 0; i < waveLenght; i++)
        {
            bool paused = false;
            //Debug.Log(random);
            if (GameManager.IsPaused)
            {
                i--;
                paused = true;
                Debug.Log("waiting");

                yield return new WaitUntil(() => !GameManager.IsPaused);

            }
            else
            {
                if (paused == true)
                {
                    paused = false;
                    yield return new WaitForSeconds(timePassed);
                }
                timePassed = 0;
                EnemyShip tempShip = Instantiate<EnemyShip>(ship, transform);
                tempShip.SetWayPoints(waypointsParents[wayPointIndex]);

                int random = Random.Range(0, allItems.Count);
                tempShip.SetItem(allItems[random], this);
                emeniesAlive.Add(tempShip);
                yield return new WaitForSeconds(0.4f);
            }

        }
        wayPointIndex++;
        isDoneSpawning = true;

    }

}

