using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Astroide astroide;
    float tempTime = 2f;
    float tempTimer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tempTimer += Time.deltaTime;

        if(tempTimer >= tempTime)
        {
            Instantiate<Astroide>(astroide, transform);
            tempTimer = 0;
        }
    }
}
