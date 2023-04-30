using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserGunEnable : ItemBase
{
    public override string Name
    {
        get
        {

            return gameObject.name;


        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 2;
        base.OnUse();
        

    }
}