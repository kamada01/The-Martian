using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillCount : MonoBehaviour
{
    public Text counter;
    int kills = 0;

    public void AddKill()
    {

        kills++;
    }

    public void ShowKills()
    {
        counter.text = kills.ToString();
    }
}


// KillCount killcountscript;
// Start() : killcountscript = GameObject.Find("KillCount").GetComponent<KillCount>();
// Destroy: killcountscript.AddKill();
