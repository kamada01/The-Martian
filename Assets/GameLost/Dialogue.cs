using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] message;
    public float Speed;
    private int idx;
    public GameObject btn;

    private void StartDialogue()
    {
        idx = 0;
        StartCoroutine(TypeLine());
    }
    
    // enumerate char one by one at specified speed
    private IEnumerator TypeLine()
    {
        foreach (char c in message[idx].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(Speed); ;
        }
    }

    private void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    private void NextLine()
    {
        if(idx < message.Length - 1)
        {
            idx++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            btn.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(text.text == message[idx])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = message[idx];
            }
        }
    }
}
