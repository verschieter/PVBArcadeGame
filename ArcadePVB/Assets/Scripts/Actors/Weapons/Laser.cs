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
    public float chargingTimeAmount;
    public float chargeMultiplier;
    int chargeMax = 3;
    public Vector3 lineSize;
    RaycastHit2D[] raycastHit = new RaycastHit2D[5];

    //Laser 
    float sizeTime;
    float lifeSpan = 0.25f;
    float laserLength = 5f;
    float startTimeOffest = 0.25f;

    //collision
    public BoxCollider2D laserCollider;
    protected override void Start()
    {
        base.Start();

        //Get the LineRenderer and set it invisble
        lineLaser = GetComponent<LineRenderer>();
        lineLaser.enabled = false;


        //set starting size, turn laser collider off and set the starting time.
        ChargeObject.transform.localScale = lineSize;
        laserCollider.enabled = false;
        sizeTime = chargingTimeAmount - startTimeOffest;


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
                if (sizeTime >= chargingTimeAmount && chargeTimes < chargeMax)
                {
                    //scales size, reset sizeTime, set the chargeObject to new size and adds 1 to the chargeTimes
                    lineSize *= chargeMultiplier;
                    sizeTime = 0;
                    ChargeObject.transform.localScale = lineSize;
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
            impact.Play();
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
        int hits = Physics2D.RaycastNonAlloc(transform.position + new Vector3(0, 0.5f), Vector2.up, raycastHit);

        for (int i = 0; i < hits; i++)
        {
            if (raycastHit[i].transform.gameObject.layer == layer)
            {
                BoxCollider2D box = raycastHit[i].transform.GetComponent<BoxCollider2D>();

                float yLower = raycastHit[i].transform.position.y - (box.bounds.center.y - box.bounds.extents.y);
                laserLength = raycastHit[i].transform.position.y - yLower - transform.position.y;
            }
        }

        //turn on the linerederer and turn of the chargeObject
        lineLaser.enabled = true;
        ChargeObject.SetActive(false);

        //set the width of the lineLaser start and end to size with a offset
        lineLaser.startWidth = lineSize.x / 5;
        lineLaser.endWidth = lineSize.x / 5;

        //set the length of the lineLaser
        lineLaser.SetPosition(1, new Vector3(0, laserLength, 1));

        //set the size of the laserCollider to match the viseule of the LineRederer and enables collision
        laserCollider.size = new Vector2(lineLaser.startWidth, laserLength);
        laserCollider.offset = new Vector2(0, laserLength / 2);
        laserCollider.enabled = true;
    }
    public void SetCharge(bool charge)
    {
        //turn the charging on or off
        isCharging = charge;
    }


}
