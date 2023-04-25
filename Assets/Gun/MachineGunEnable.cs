using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnable : ItemBase
{
    public override string Name
    {
        get
        {
            
            return "MachineGun";
            
            
        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 3;
        base.OnUse();
        

    }
}