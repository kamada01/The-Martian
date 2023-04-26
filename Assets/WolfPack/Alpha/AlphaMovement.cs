using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] GameObject beta;
    [SerializeField] private float vision;
    [SerializeField] private Astronaut player;
    GameObject memberA, memberB;

    private Vector2 DirectionToPlayer;

    private float cdTimer = Mathf.Infinity;
    private float HP = 3;

    Animator animator;
    private Rigidbody2D rb;
    private RaycastHit2D hit;

    private void Start()
    {
        player = (Astronaut)FindObjectOfType(typeof(Astronaut));    
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //visionController = GetComponent<AlphaVisionController>();
        //SummonPack();
        StartCoroutine(SummonPack());

    }

    private void Update()
    {
        cdTimer += Time.deltaTime;

        if (PlayerWithinAttackRange())
        {
            //Debug.Log("Player within attack range.");
            if (cdTimer >= attackCD)
            {
                cdTimer = 0;
                //Debug.Log("Launching attack");
                animator.SetTrigger("attack");

                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            MovingTowardsPlayer();
        }

    }
    private IEnumerator AttackPlayer()
    {
        // Wait for 1 second 
        yield return new WaitForSeconds(1f);
        // only reduce player's HP if the player is still within the attacking range after 1 sec
        /*
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Reduce Player's HP in a sub
            //DamagePlayer(damage);
            //debug message
            //Debug.Log("Attack causes damage");
        }
        */
        DamagePlayer(damage);
    }

    private bool PlayerWithinAttackRange()
    {
        //create a box collider representing the attack range 
        hit = Physics2D.BoxCast(boxCollider.bounds.center +
            transform.right * attackRange * transform.localScale.x*1.3f+Vector3.down*0.7f,
            boxCollider.bounds.size*0.6f, 0, Vector2.left, 0);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Debug.Log("Player within attack range.");
            return true;
        }
        else
        {
            return false;
        }     
    }

    private void OnDrawGizmos()
    {
        //draw the boxcollider representing the attacking range 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center +
            transform.right * attackRange * transform.localScale.x * 1.3f + Vector3.down * 0.7f,
            boxCollider.bounds.size * 0.6f);
    }

    private void DamagePlayer(int damageCaused)
    {
        //To be implement after setting up the player's hp codes
        player.TakingDamage(damageCaused);
        player.damagePopup(damageCaused);
    }

    public void TakingDamage(float damageTaken)
    {
        //To be implement after settig up the player's weapons
        HP = HP - damageTaken;

        if (HP <= 0)
        {
            Destroy(memberA);
            Destroy(memberB);
            Destroy(gameObject);
        }
    }

    private void MovingTowardsPlayer()
    {
        if (AwareOfPlayer())
        {
            //targetDirection = visionController.DirectionToPlayer;
            animator.SetTrigger("move");
            //Debug.Log("Player detected. Moving towards the player");

            //Flip the spirte if necessary
            if (DirectionToPlayer.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (DirectionToPlayer.x < 0)
            {
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.AddForce(DirectionToPlayer * speed * Time.deltaTime);

        }
        else
        {
            DirectionToPlayer = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("idle");
            //Debug.Log("Player Not detected.");
        }

    }

    private bool AwareOfPlayer()
    {   //check if the player is within the "detection range"
        Vector2 enemyToPlayerVector = player.transform.position - transform.position;

        if (enemyToPlayerVector.magnitude <= vision)
        {
            DirectionToPlayer = enemyToPlayerVector.normalized;
            return true;
        }
        else
        {
            return false;
        }

    }

    private IEnumerator SummonPack()
    {
        // Wait for 7 second 
        yield return new WaitForSeconds(7f);

        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;

        // Randomly pick a point within the spawn object 
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Create an enemy at the 'spawnPoint' position
        memberA = Instantiate(beta, spawnPoint, Quaternion.identity);

        // Assign the player variable of the new beta object to the player variable of this AlphaMovement script
        memberA.GetComponent<BetaMovement>().player = player;

        // Randomly pick a point within the spawn object 
        spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Create an enemy at the 'spawnPoint' position
        memberB = Instantiate(beta, spawnPoint, Quaternion.identity);

        // Assign the player variable of the new beta object to the player variable of this AlphaMovement script
        memberB.GetComponent<BetaMovement>().player = player;
    }


    /*void SummonPack()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;

        // Randomly pick a point within the spawn object 
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Create an enemy at the 'spawnPoint' position
        memberA = Instantiate(beta, spawnPoint, Quaternion.identity);

        // Assign the player variable of the new beta object to the player variable of this AlphaMovement script
        memberA.GetComponent<BetaMovement>().player = player;

        // Randomly pick a point within the spawn object 
        spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        // Create an enemy at the 'spawnPoint' position
        memberB = Instantiate(beta, spawnPoint, Quaternion.identity);

        // Assign the player variable of the new beta object to the player variable of this AlphaMovement script
        memberB.GetComponent<BetaMovement>().player = player;

    }*/


}
