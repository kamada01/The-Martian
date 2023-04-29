using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGunEnable : ItemBase
{
    /*public GameObject Hand;
    public GameObject Astronaut;*/
    private GameObject handObject;
    private GameObject astronautObject;


    public override string Name
    {
        get
        {
            /*Hand = GameObject.FindWithTag("hand");
            Astronaut = GameObject.FindWithTag("Astronaut");*/
            handObject = transform.parent.Find("Hand").gameObject;
            astronautObject = transform.parent.parent.Find("Astronaut").gameObject;

            return gameObject.name;


        }
    }

    public override void OnUse()
    {
        GlobalVariables.gunEnabled = 4;
        base.OnUse();
        

    }
}