using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawn : MonoBehaviour
{
    public GameObject[] guns; // Array of gun prefabs to spawn
    public GameObject[] gunEffect; // 
    public GameObject topRightLimit; // Game object representing the top right limit of the map
    public GameObject bottomLeftLimit; // Game object representing the bottom left limit of the map
    private bool allGunsSpawned = false; // Flag indicating if all guns have been spawned
    private float spawnInterval = 10f; // Time between gun spawns in seconds
    private float timer = 0f; // Timer to track time since last gun spawn
    private int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the timer to the spawn interval so a gun spawns immediately on start
        timer = spawnInterval;
        //Debug.Log(guns.Length);

    }

    // Update is called once per frame
    void Update()
    {
        // Only spawn guns if all guns haven't already been spawned
        if (!allGunsSpawned)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if enough time has passed to spawn another gun
            if (timer >= spawnInterval)
            {
                // Reset the timer
                timer = 0f;

                // Spawn a random gun prefab at a random location within the map limits
                GameObject gunPrefab = guns[counter];
                Vector2 spawnPosition = new Vector3(
                    Random.Range(bottomLeftLimit.transform.position.x, topRightLimit.transform.position.x),
                    Random.Range(bottomLeftLimit.transform.position.y, topRightLimit.transform.position.y)
                );
                Instantiate(gunPrefab, spawnPosition, Quaternion.identity);

                //Debug.Log("Generate gun " + counter);

                counter = counter +1;
                

                // Check if all guns have been spawned
                if (guns.Length == counter)
                {
                    //Debug.Log("Generated all guns");

                    allGunsSpawned = true;
                    spawnInterval = 1000f;
                }
            }
        }
    }

}
