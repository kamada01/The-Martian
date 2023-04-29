using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    [SerializeField] private float spawnRate = 8f;
    [SerializeField] private GameObject[] potions;
    [SerializeField] private bool canSpawn = true;

    /*
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    */

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, potions.Length - 1);
            GameObject potiontoSpawn = potions[rand];
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 pos = new Vector2(spawnX, spawnY);
            Instantiate(potiontoSpawn, pos, Quaternion.identity);
        }
    }
}
