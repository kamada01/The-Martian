using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGunEnable : ItemBase
{
    public override string Name
    {
        get
        {
            
            return "shotGun";
            
            
        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 4;
        base.OnUse();
        

    }
}