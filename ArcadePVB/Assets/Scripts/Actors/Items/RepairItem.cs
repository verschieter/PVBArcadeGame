using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : Item
{
    public int repairAmount = 15;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
          
        player.ChangeHealth(-repairAmount);
        player.activeItems.Remove(this);
        ResetEffect();
    }
    protected override void ResetEffect()
    {

        base.ResetEffect();


    }

}
