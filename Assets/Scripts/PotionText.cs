using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionText : MonoBehaviour
{
    public Text textbox;

    public void Write(string content)
    {
        textbox.text = content;
    }
}
