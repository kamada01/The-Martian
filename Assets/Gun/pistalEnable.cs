using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistalEnable : ItemBase
{
    public override string Name
    {
        get
        {
            
            return "Pistal";
            
            
        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 1;
        base.OnUse();
        

    }
}