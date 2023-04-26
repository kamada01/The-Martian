using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bulletScript : MonoBehaviour
{

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Set the vertical speed to make the bullet move upward
       /* Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 v = rb.velocity;
        v.y = 6;
        rb.velocity = v;*/
    }

    // Gets called when the object goes out of the screen
    void OnBecameInvisible()
    {
        // Destroy the bullet
        Destroy(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Vector3 CurPos = gameObject.transform.position;
            DamagePopup.Create(CurPos, damage);
            //get the name of the enemy
            string enemyName = collision.gameObject.name;
            if (enemyName.Contains("(Clone)"))
            {
                enemyName = enemyName.Replace("(Clone)", "");
            }
            //process the name so that it matches the name of the script
            string enemyScriptName = enemyName + "Movement";
            enemyScriptName = enemyScriptName.Replace(" ", "");

            //Debug.Log("accessing script: " + enemyScriptName + " of " + enemyName + ".");

            //access the script
            Component enemyScript = collision.gameObject.GetComponent(enemyScriptName);
            if (enemyScript != null)
            {
                Debug.Log("Damage Enemy using method.");
                enemyScript.SendMessage("TakingDamage", damage);
            }

            Destroy(gameObject);
        }


    }

}
