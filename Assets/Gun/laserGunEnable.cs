using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserGunEnable : ItemBase
{
    public override string Name
    {
        get
        {
            
            return "laserGun";
            
            
        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 2;
        base.OnUse();
        

    }
}