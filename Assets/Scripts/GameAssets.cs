using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Transform DamagePopup;
    public Transform Astronaut;
    public Transform Hand;

    private static GameAssets asset;

    public static GameAssets getAsset
    {
        get
        {
            if (asset == null) asset = Instantiate(Resources.Load<GameAssets>("Game Assets"));
            return asset;
        }
    }
}
