using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    // Variable to store the enemy prefab
    public GameObject enemy;
    public GameObject enemy_two;


    //store the player
    public Transform player;

    // Variable to know how fast we should create new enemies
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // Call the 'addEnemy' function every 'spawnTime' seconds
        InvokeRepeating("AddEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // New function to spawn an enemy
    void AddEnemy()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;

        // Randomly pick a point within the spawn object 
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Create an enemy at the 'spawnPoint' position
        GameObject temp = Instantiate(enemy, spawnPoint, Quaternion.identity);

        string enemyName = enemy.gameObject.name;
        //process the name so that it matches the name of the script
        enemyName = enemyName.Replace("(Clone)", "");

        string enemyScriptName = enemyName + "Movement";
        enemyScriptName = enemyScriptName.Replace(" ", "");

        //access the script
        Component enemyScript = temp.GetComponent(enemyScriptName);

        if (enemyScript != null)
        {
            // assign the player object to the enemy
            //Debug.Log("enemyScript not null");
            //call the SetPlayer method in the enemy script
            enemyScript.SendMessage("SetPlayer", player);

            if (enemyName.Contains("Alpha") && enemy_two != null)
            {
                //Debug.Log("Assigning Beta");
                enemyScript.SendMessage("SetBeta", enemy_two);
                enemyScript.SendMessage("SummonPack");

            }

        }




    }

}
