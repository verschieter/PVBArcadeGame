using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laser : MonoBehaviour
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

    //Laser 
    float sizeTime;
    float lifeSpan = 0.25f;
    float laserLength = 1.2f;
    float startTimeOffest = 0.25f;

    //collision
    public BoxCollider2D LaserCollider;
    Player player;
    public int damage;
    void Start()
    {
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

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        if (!GameManager.IsPaused)
        {

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
    public void UnleachCharge()
    {
        //turn on the linerederer and turn of the chargeObject
        lineLaser.enabled = true;
        ChargeObject.SetActive(false);

        //set the width of the lineLaser start and end to size with a offset
        lineLaser.startWidth = size.x /5;
        lineLaser.endWidth = size.x / 5;

        //set the length of the lineLaser
        lineLaser.SetPosition(1, new Vector3(0, laserLength, 1));

        //set the size of the laserCollider to match the viseule of the LineRederer and enables collision
        LaserCollider.size = new Vector2(lineLaser.startWidth, laserLength);
        LaserCollider.enabled = true;
    }
    public void SetCharge(bool charge)
    {
        //turn the charging on or off
        isCharging = charge;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        //for later to makes sure only hits this layer
        int layerMask = 1 << 6;
        layerMask = ~layerMask;


        if (col != null && col.gameObject.layer != LayerMask.NameToLayer("Block"))
        {
          
            if (col.gameObject.layer == LayerMask.NameToLayer("Astroide"))
            {
                Astroide astroide = col.gameObject.GetComponent<Astroide>();
                astroide.Destroyed();
                player.AddScore(astroide.scorePoint);
            }
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + col.transform.name);

            //get the locaction of correct hit point between laser and hitObject.
            float offsetY = col.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            float offsetX = transform.position.x;
            Vector3 hitPoint = col.gameObject.transform.position - new Vector3(-offsetX, offsetY);

            //resize the laser to not extend further then hitpoint
            //and set the location of impactObject to the same position
            Vector3 LineLength = new Vector3(0, hitPoint.y - transform.position.y, 1);
            lineLaser.SetPosition(1, LineLength);
            Destroy(col.gameObject);
        }

        transform.SetParent(null);

    }

}