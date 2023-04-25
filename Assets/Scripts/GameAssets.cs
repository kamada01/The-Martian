using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public Transform DamagePopup;
    public GameObject Astronaut;
    public GameObject Hand;

    private static GameAssets asset;

    public static GameAssets getAsset
    {
        get
        {
            if (asset == null) asset = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return asset;
        }
    }
}
