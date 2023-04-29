using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawn1 : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private bool spawn = true;
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;


    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (spawn)
        {
            yield return wait;
            int rand = Random.Range(0, guns.Length);
            GameObject gunSpawn = guns[rand];

            Vector3 randomPos = new Vector3(
                Random.Range(minY, maxY),
                Random.Range(minY, maxY),
                0
            );

            Instantiate(gunSpawn, randomPos, Quaternion.identity);
        }
    }
}
