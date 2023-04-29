using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class AlphaMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] public GameObject beta;
    [SerializeField] private float vision;
    [SerializeField] private Transform player;
    GameObject memberA, memberB;
    GameObject[] packMember;
    private int packSize;

    private Vector2 DirectionToPlayer;

    private float cdTimer = Mathf.Infinity;
    public int HP = 3;
    private int damage = 2;

    Animator animator;
    private Rigidbody2D rb;
    private RaycastHit2D hit;
    private KillCount killcount;

    public AudioClip breathingSound;
    //private AudioSource audioSource;

   /* private void Start()
    {
        killcount = GameObject.Find("KillCount").GetComponent<KillCount>();
    }*/

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        killcount = GameObject.Find("KillCount").GetComponent<KillCount>();
        if (beta != null)
        {
            Debug.Log(packSize);
            packSize = Random.Range(0, 7);
            packMember = new GameObject[packSize];
            StartCoroutine(SummonPack());
        }

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
        // Wait for 0.7 second 
        yield return new WaitForSeconds(0.7f);
        // only reduce player's HP if the player is still within the attacking range after 0.7 sec
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Reduce Player's HP in a sub
            DamagePlayer(damage);
            //Debug.Log("Attack causes damage");
        }
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
    {//use the TakingDamage method in player
        player.GetComponent<Astronaut>().TakingDamage(damageCaused);
    }
    

    public void TakingDamage(int damageTaken)
    {
        animator.SetTrigger("hurt");
        HP = HP - damageTaken;
        //Debug.Log("Hurt by Player. Current HP: "+HP);

        if (HP <= 0)
        {   //stopped all the movement
            DirectionToPlayer = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("death");
            //Debug.Log("Death Triggered ");
            /*Destroy(memberA);
            Destroy(memberB);*/
            for (int i = 0; i < packSize; i++)
            {
                Destroy(packMember[i]);
            }
            killcount.AddKill();
        }
    }

    public void DestroyAfterAnimation()
    {   //called by the last frame in the Death animation clip
        Destroy(gameObject);
    }

    private void MovingTowardsPlayer()
    {
        if (AwareOfPlayer())
        {
            //targetDirection = visionController.DirectionToPlayer;
            animator.SetTrigger("move");
            //Debug.Log("Player detected. Moving towards the player");

            //audioSource.PlayOneShot(breathingSound);
            SoundManager.Instance.AlphaPlay(breathingSound);


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
        Vector2 enemyToPlayerVector = player.position - transform.position;

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
        // Wait for 5 seconds 
        yield return new WaitForSeconds(5f);

        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        // Variables to store the X position of the spawn object
        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;
        //Debug.Log(packSize);
        for (int i = 0; i < packSize; i++)
        {

            // Randomly pick a point within the spawn object 
            Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

            // Create an enemy at the 'spawnPoint' position
            packMember[i] = Instantiate(beta, spawnPoint, Quaternion.identity);

            // Assign the player variable of the new beta object to the player variable of this AlphaMovement script
            packMember[i].GetComponent<BetaMovement>().player = player;
        }


        /*enderer rd = GetComponent<Renderer>();
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
        memberB.GetComponent<BetaMovement>().player = player;*/
    }

    public void SetPlayer(Transform playerObject)
    {//only used by the Spawn to pass its player object
        player = playerObject;

    }
    public void SetBeta(GameObject enemyObject)
    {//only used by the Spawn to pass its player object
        beta = enemyObject;
        

    }
    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 100;
        } 
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 0;
        }
    }*/

}
