using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public static void Init()
    {
        Debug.Log("GameLogic.Init");

        CreateSprite();
    }

    private static void CreateSprite()
    {
        Debug.Log("CreateSprite");
    }
}
