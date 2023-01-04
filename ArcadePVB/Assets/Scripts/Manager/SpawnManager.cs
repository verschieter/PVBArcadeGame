using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Astroide astroide;
    public EnemyShip ship;

    public List<Transform> waypointsParents = new List<Transform>();

    float tempTime = 1f;
    float tempTimer = -2;

    public List<Item> allItems = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempTimer += Time.deltaTime;

        if (tempTimer >= tempTime)
        {
            int random = Random.Range(0, 4);
            Debug.Log(random);


            Astroide tempAstroide = Instantiate<Astroide>(astroide, transform);
            EnemyShip tempShip = Instantiate<EnemyShip>(ship, transform);
            tempShip.SetWayPoints(waypointsParents[0]);
            

            if (random == 3)
            {
                random = Random.Range(0, allItems.Count);
                tempAstroide.SetItem(allItems[random]);
            }


            tempTimer = 0;
        }
    }
}
