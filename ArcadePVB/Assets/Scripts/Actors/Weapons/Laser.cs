using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Weapon
{
    //visuale object reference
    public GameObject ChargeObject;
    LineRenderer lineLaser;

    //Charging 
    public bool isCharging;
    int chargeTimes;
    public float chargingTime;
    public float chargeMultiplier;
    int chargeMax = 3;
    public Vector3 size;
    RaycastHit2D[] hit = new RaycastHit2D[5];

    //Laser 
    float sizeTime;
    float lifeSpan = 0.25f;
    float laserLength = 5f;
    float startTimeOffest = 0.25f;

    //collision
    public BoxCollider2D LaserCollider;
    protected override void Start()
    {
        base.Start();

        //Get the LineRenderer and set it invisble
        lineLaser = GetComponent<LineRenderer>();
        lineLaser.enabled = false;


        //set starting size, turn laser collider off and set the starting time.
        ChargeObject.transform.localScale = size;
        LaserCollider.enabled = false;
        sizeTime = chargingTime - startTimeOffest;


        //Start the charging
        SetCharge(true);
    }

    protected override void Update()
    {
        if (!GameManager.IsPaused)
        {

            base.Update();
            //checks to see if the player is still holding charge or the charge hasnt reach the first stage yet
            if (isCharging || chargeTimes <= 0)
            {
                //make sure sizeTime keep counting up without frame delay.
                sizeTime += Time.deltaTime;

                //Checks if the SizeTime has reached charginTime and makes sure chargeTimes doesnt get higher then the chargeMax
                if (sizeTime >= chargingTime && chargeTimes < chargeMax)
                {
                    //scales size, reset sizeTime, set the chargeObject to new size and adds 1 to the chargeTimes
                    size *= chargeMultiplier;
                    sizeTime = 0;
                    ChargeObject.transform.localScale = size;
                    chargeTimes++;
                }

            }
            else
            {
                //when player isnt holding charge and chargetime is higher then the first stage we will then Call UnleachCharge
                //and set a Life span timer which when depleted destroys the gameobject

                UnleachCharge();

                lifeSpan -= Time.deltaTime;
                if (lifeSpan <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

    }
    public override void HitBlock()
    {
        return;
    }

    public override void OnEnemyCollision(GameObject enemyObject)
    {
        if (enemyObject != null && enemyObject.layer != LayerMask.NameToLayer("BlockB"))
        {
            base.OnEnemyCollision(enemyObject);
            damage -= 10;
            Impact.Play();
        }
        transform.SetParent(null);

    }

    public void UnleachCharge()
    {
        LayerMask layer = LayerMask.NameToLayer("BlockB");
        if (source.clip != sounds[1])
            source.clip = sounds[1];
        if (!source.isPlaying)
            source.Play();
        int hits = Physics2D.RaycastNonAlloc(transform.position + new Vector3(0, 0.5f), Vector2.up, hit);

        for (int i = 0; i < hits; i++)
        {
            if (hit[i].transform.gameObject.layer == layer)
            {
                BoxCollider2D box = hit[i].transform.GetComponent<BoxCollider2D>();

                float yLower = hit[i].transform.position.y - (box.bounds.center.y - box.bounds.extents.y);
                laserLength = hit[i].transform.position.y - yLower - transform.position.y;
            }
        }

        //turn on the linerederer and turn of the chargeObject
        lineLaser.enabled = true;
        ChargeObject.SetActive(false);

        //set the width of the lineLaser start and end to size with a offset
        lineLaser.startWidth = size.x / 5;
        lineLaser.endWidth = size.x / 5;

        //set the length of the lineLaser
        lineLaser.SetPosition(1, new Vector3(0, laserLength, 1));

        //set the size of the laserCollider to match the viseule of the LineRederer and enables collision
        LaserCollider.size = new Vector2(lineLaser.startWidth, laserLength);
        LaserCollider.offset = new Vector2(0, laserLength / 2);
        LaserCollider.enabled = true;
    }
    public void SetCharge(bool charge)
    {
        //turn the charging on or off
        isCharging = charge;
    }


}
