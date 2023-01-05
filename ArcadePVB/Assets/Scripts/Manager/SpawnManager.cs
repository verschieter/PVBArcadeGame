using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Astroide astroide;
    public EnemyShip ship;

    public List<Transform> waypointsParents = new List<Transform>();

    Timer astroideTimer;
    Timer enemyTimer;

    float waveLenght = 5;
    bool isDoneSpawning = true;

    public List<Item> allItems = new List<Item>();

    public SpawnManager()
    {
        astroideTimer = new Timer();
        enemyTimer = new Timer();
    }
    // Start is called before the first frame update
    void Start()
    {
        astroideTimer.StartTimer(0.8f, 4.6f);
        enemyTimer.StartTimer(0.8f, 4.6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsPaused)
        {
            SpawmnEnemy();

            SpawnAstroide();
        }

    }
    private void SpawnAstroide()
    {

        if (astroideTimer.IsDone())
        {
            int random = Random.Range(0, 4);

            Vector2 spawnPos = new Vector2(Random.Range(-2.69f, 2.69f), transform.position.y);
            Astroide tempAstroide = Instantiate<Astroide>(astroide, spawnPos, Quaternion.identity);

            //if (random == 3)
            //{
                random = Random.Range(0, allItems.Count);
                tempAstroide.SetItem(allItems[random]);
            //}
            astroideTimer.StartTimer(2f, 4.6f);
        }
    }

    private void SpawmnEnemy()
    {
        if (enemyTimer.IsDone() && isDoneSpawning)
        {
            isDoneSpawning = false;
            StartCoroutine(SpawnShip());
        }
    }
    IEnumerator SpawnShip()
    {
        for (int i = 0; i < waveLenght; i++)
        {
            int random = Random.Range(0, 4);
            //Debug.Log(random);

            EnemyShip tempShip = Instantiate<EnemyShip>(ship, transform);
            tempShip.SetWayPoints(waypointsParents[0]);
            if (random == 3)
            {
                random = Random.Range(0, allItems.Count);
                tempShip.SetItem(allItems[random]);
            }
            yield return new WaitForSeconds(0.4f);
        }
        enemyTimer.StartTimer(1.8f, 4.6f);
        isDoneSpawning = true;

    }

}

