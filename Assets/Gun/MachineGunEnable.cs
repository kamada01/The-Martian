using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnable : ItemBase
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
        GlobalVariables.gunEnabled = 3;
        base.OnUse();
        

    }
}