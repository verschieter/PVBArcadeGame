using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    public Astroide astroide;
    public List<EnemyShip> enemyShips = new List<EnemyShip>();

    public BGMove bgMove;

    public List<Transform> waypointsSmallEnemies = new List<Transform>();
    public List<Transform> waypointsMediumEnemies = new List<Transform>();

    Timer astroideTimer;
    Timer enemyTimer;

    bool isDoneSpawning = true;

    public List<Item> allItems = new List<Item>();

    List<Enemy> emeniesAlive = new List<Enemy>();

    bool gameEnded;
    float totalDistance;
    float spawnDistance;
    float spawnDivider = 25;
    int wayPointIndex;

    float timePassed;
    bool hasIncreaseDifficulty;
    bool smallEnemiesHasSpawned;

    float IncreaseMulitplier = 1.5f;

    float[] astroideSpawnTimes = new float[2] { 2.5f, 4.5f };
    public SpawnManager()
    {
        astroideTimer = new Timer();
        enemyTimer = new Timer();
    }
    // Start is called before the first frame update
    void Start()
    {
        totalDistance = bgMove.DistanceToFinish();

        spawnDistance = totalDistance / spawnDivider;

        astroideTimer.StartTimer(0.8f, 4.6f);
        enemyTimer.StartTimer(0.8f, 4.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused && gameEnded == false)
        {
            if (astroideTimer.IsTimerPause())
                astroideTimer.PauseTimer();

            SpawnAstroide();

            float currentDistance = bgMove.DistanceToFinish();

            if (currentDistance <= totalDistance / 2f && !hasIncreaseDifficulty)
                IncreaseDifficulty();

            if (currentDistance <= totalDistance - spawnDistance)
            {
                if (hasIncreaseDifficulty)
                    SpawnMediumEnemies();

                SpawmnSmallEnemies();
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
            Vector2 spawnPos = new Vector2(Random.Range(-2.6f, 2.6f), transform.position.y);
            Astroide tempAstroide = Instantiate<Astroide>(astroide, spawnPos, Quaternion.identity);

            int random = Random.Range(0, allItems.Count);
            tempAstroide.SetItem(allItems[random], this);

            if (hasIncreaseDifficulty)
            {
                tempAstroide.damage *= IncreaseMulitplier;
                tempAstroide.health *= IncreaseMulitplier;
            }

            astroideTimer.StartTimer(astroideSpawnTimes[0], astroideSpawnTimes[1]);
            emeniesAlive.Add(tempAstroide);
        }
    }

    public void IncreaseDifficulty()
    {
        hasIncreaseDifficulty = true;
        spawnDivider += 10;
    }
    private void SpawnMediumEnemies()
    {
        if (isDoneSpawning && smallEnemiesHasSpawned)
        {
            isDoneSpawning = false;
            StartCoroutine(SpawnShip(enemyShips[1], 3, waypointsMediumEnemies, 0.4f));
            smallEnemiesHasSpawned = false;
        }
    }
    private void SpawmnSmallEnemies()
    {
        if (isDoneSpawning)
        {
            isDoneSpawning = false;
            StartCoroutine(SpawnShip(enemyShips[0], 6, waypointsSmallEnemies, 0.25f));
            smallEnemiesHasSpawned = true;
        }
    }

    public bool AllEnmiesDied()
    {
        return emeniesAlive.Count == 0 && gameEnded;
    }

    public void RemoveFromList(Enemy enemy)
    {
        emeniesAlive.Remove(enemy);
    }

    IEnumerator SpawnShip(EnemyShip enemyship, int waveLenght, List<Transform> waypoints, float spawnRate)
    {
        if (enemyship)
        {
            wayPointIndex = Random.Range(0, waypoints.Count);
            for (int i = 0; i < waveLenght; i++)
            {
                bool paused = false;
                if (GameManager.IsPaused)
                {
                    i--;
                    paused = true;
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
                    EnemyShip tempShip = Instantiate<EnemyShip>(enemyship, transform);
                    tempShip.SetWayPoints(waypoints[wayPointIndex]);

                    int random = Random.Range(0, allItems.Count);
                    tempShip.SetItem(allItems[random], this);

                    emeniesAlive.Add(tempShip);
                    yield return new WaitForSeconds(spawnRate);
                }
            }
        }
        isDoneSpawning = true;
    }
}

