using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmCount : MonoBehaviour
{
    public Text counter;

    public void updateCount(int cur, int max)
    {
        counter.text = cur.ToString() + " / " + max.ToString();
    }

    public void colorRed()
    {
        counter.color = Color.red;
    }

    public void colorBlack()
    {
        counter.color = Color.black;
    }
}
