using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private Potiontype potionType;
    private Astronaut astronaut;
    private PotionText text;


    public void Start()
    {
        astronaut = (Astronaut)FindAnyObjectByType(typeof(Astronaut));
        text = (PotionText)FindAnyObjectByType(typeof(PotionText));

    }

    public enum Potiontype
    {
        AddHp, // + hp
        DeductHp, // - hp
        Freeze, // freeze x y position
        AddSpeed, // increase player movementSpeed
        DeductSpeed, // decrease player movementSpeed
        Invisibility, // shift to layer without collision detection
        Grow, // enlarge scale
        Shrink // decrease scale
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.GetComponent<Potion>().potionType == Potiontype.AddHp)
            {
                AddHp();
                Destroy(gameObject);
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.DeductHp)
            {
                DeductHp();
                Destroy(gameObject);
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.AddSpeed)
            {
                StartCoroutine(AddSpeed());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.DeductSpeed)
            {
                StartCoroutine(DeductSpeed());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.Invisibility)
            {
                StartCoroutine(Invisible());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.Freeze)
            {
                StartCoroutine(Freeze());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.Grow)
            {
                StartCoroutine(Grow());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            else if (gameObject.GetComponent<Potion>().potionType == Potiontype.Shrink)
            {
                StartCoroutine(Shrink());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
        }
    }

    private void AddHp()
    {
        int num = Random.Range(1, 10);
        if (astronaut.CurHealth + num > astronaut.MaxHealth)
        {
            astronaut.CurHealth = astronaut.MaxHealth;
        } else
        {
            astronaut.CurHealth += num;
        }
        text.Write("HP Potion! Added " + num.ToString() + " HP!");
    }

    private void DeductHp()
    {
        int num = Random.Range(1, 10);
        astronaut.CurHealth -= num;
        text.Write("Poison Potion! Lost " + num.ToString() + " HP!");
    }

    IEnumerator AddSpeed()
    {
        int num = Random.Range(2, 4);
        astronaut.movementSpeed += num;
        int time = Random.Range(5, 10);
        text.Write("Speedup Potion! +" + num.ToString() + " speed for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        text.Write("");
        astronaut.movementSpeed -= num;
        Destroy(gameObject);
    }

    IEnumerator DeductSpeed()
    {
        int num = Random.Range(1, 3);
        astronaut.movementSpeed -= num;
        int time = Random.Range(5, 10);
        text.Write("Slowdown Potion! -" + num.ToString() + " speed for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        text.Write("");
        astronaut.movementSpeed += num;
        Destroy(gameObject);
    }

    IEnumerator Invisible()
    {
        int time = Random.Range(3, 8);
        int layer = LayerMask.NameToLayer("Invisible");
        astronaut.gameObject.layer = layer;
        text.Write("Invisible Potion! You can run through enemies for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        text.Write("");
        layer = LayerMask.NameToLayer("Default");
        astronaut.gameObject.layer = layer;
        Destroy(gameObject);
    }

    IEnumerator Freeze()
    {
        int time = Random.Range(3, 8);
        astronaut.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        text.Write("Freeze Potion! You cannot move for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        astronaut.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        text.Write("");
        Destroy(gameObject);
    }

    IEnumerator Grow()
    {
        astronaut.transform.localScale += new Vector3(0.5f, 0.5f, 0f);
        int time = Random.Range(5, 10);
        text.Write("Growth Potion! You are larger for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        text.Write("");
        astronaut.transform.localScale -= new Vector3(0.5f, 0.5f, 0f);
        Destroy(gameObject);
    }

    IEnumerator Shrink()
    {
        astronaut.transform.localScale -= new Vector3(0.2f, 0.2f, 0f);
        int time = Random.Range(5, 10);
        text.Write("Growth Potion! You are smaller for " + time.ToString() + " seconds!");
        yield return new WaitForSeconds(time);
        text.Write("");
        astronaut.transform.localScale += new Vector3(0.2f, 0.2f, 0f);
        Destroy(gameObject);
    }
}
