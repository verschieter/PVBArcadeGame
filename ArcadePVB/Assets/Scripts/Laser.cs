using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Laser : MonoBehaviour
{
    //visuals
    public GameObject ChargeObject;
    public GameObject ImpactObject;
    LineRenderer line;
    List<Vector2> colliderPoints;
    public BoxCollider2D box;
    float laserLength = 2f;


    //Charge
    public bool isCharging;
    int chargeTimes;
    public float chargingTime;
    public float chargeMultiplier;
    int chargeMax = 3;

    public Vector3 size;
    float sizeTime;
    float lifeSpan = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        ImpactObject.SetActive(false);
        Charge();
        ChargeObject.transform.localScale = size;
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            sizeTime += Time.deltaTime;

            if (sizeTime >= chargingTime && chargeTimes < chargeMax)
            {
                size *= chargeMultiplier;
                sizeTime = 0;
                ChargeObject.transform.localScale = size;
                chargeTimes++;
            }

        }
        else
        {
            lifeSpan -= Time.deltaTime;
            if (lifeSpan <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
    public void UnleachCharge()
    {
        isCharging = false;
        line.enabled = true;
        ChargeObject.SetActive(false);

        line.startWidth = size.x / 4;
        line.endWidth = size.x / 5;

        box.size = new Vector2(line.endWidth, laserLength);
        box.enabled = true;
        
        line.SetPosition(1, new Vector3(0, laserLength, 1));
    }
    void Charge()
    {
        isCharging = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hiiiiitt");

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        ImpactObject.SetActive(true);
        if (collision != null)
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + collision.transform.name);
            float offsetY = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            float offsetX = transform.position.x;
            Vector3 hitPoint = collision.gameObject.transform.position - new Vector3(-offsetX, offsetY);


            Vector3 LineLength = new Vector3(0, hitPoint.y - transform.position.y, 1);
            line.SetPosition(1, LineLength);
            ImpactObject.transform.position = hitPoint + new Vector3(0, 0.03f);
            ImpactObject.transform.localScale = size;
        }
        
        transform.SetParent(null);

    }

}
