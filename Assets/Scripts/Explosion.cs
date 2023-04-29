using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public float interval = 5.0f;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public Astronaut astro;

    // Start is called before the first frame update
    void Start()
    {
        astro = (Astronaut)FindAnyObjectByType(typeof(Astronaut));
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(explode());
    }

    IEnumerator explode()
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        while (true)
        {
            yield return wait;

            Vector3 randompos = new Vector3(
                (float)Random.Range(minX, maxX),
                (float)Random.Range(minY, maxY),
                2
                );

            Instantiate(explosion, randompos, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int dmg = Random.Range(1, 10);
            astro.TakingDamage(dmg);
            astro.damagePopup(dmg);
        }
    }
}
