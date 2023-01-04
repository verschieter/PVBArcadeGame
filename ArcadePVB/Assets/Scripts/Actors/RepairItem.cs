using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : Item
{
    public int repairAmount = 15;
    // Start is called before the first frame update
    public override void Start()
    { 
        base.Start();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = col.gameObject.GetComponent<Player>();
            player.ChangeHealth(-repairAmount);
            Destroy(gameObject);
        }
    }

    
}
