using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float vision;
    [SerializeField] private Astronaut player;
    

    private Vector2 DirectionToPlayer;

    private float cdTimer = Mathf.Infinity;
    private float HP = 12;

    Animator animator;
    private Rigidbody2D rb;
    private RaycastHit2D hit;

    private pistal pistal;
    private shotGun mbullet;
    private laserGun laser;
    private KillCount killcountscript;
    private void Start()
    {
        player = (Astronaut)FindObjectOfType(typeof(Astronaut));
        pistal = (pistal)FindObjectOfType(typeof(pistal));
        mbullet = (shotGun)FindObjectOfType(typeof(shotGun));
        laser = (laserGun)FindObjectOfType(typeof(laserGun));
        killcountscript = GameObject.Find("KillCount").GetComponent<KillCount>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

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
            DamagePlayer(damage);
            //debug message
            Debug.Log("Attack causes damage");
        }*/
        DamagePlayer(damage);
    }

    private bool PlayerWithinAttackRange()
    {
        //create a box collider used for attack detection
        hit = Physics2D.BoxCast(boxCollider.bounds.center +
            transform.right * attackRange * transform.localScale.x,
            boxCollider.bounds.size, 0, Vector2.left, 0);

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
    {//draw the boxcollider representing the attacking range of the Minotaur
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center +
            transform.right * attackRange * transform.localScale.x,
            boxCollider.bounds.size);
    }

    private void DamagePlayer(int damageCaused)
    {
        //To be implement after setting up the player's hp  
        player.TakingDamage(damageCaused);
        player.damagePopup(damageCaused);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 CurPos = gameObject.transform.position;
        CurPos.y += 1;
        if (collision.CompareTag("PistolBullet"))
        {
            HP -= pistal.damage;
            DamagePopup.Create(CurPos, pistal.damage);
        }
        else if (collision.CompareTag("mbullet"))
        {
            HP -= shotGun.damage;
            DamagePopup.Create(CurPos, shotGun.damage);
        } else if (collision.CompareTag("laser"))
        {
            HP -= laserGun.damage;
            DamagePopup.Create(CurPos, laserGun.damage);
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
            killcountscript.AddKill();

        }
    }
    private void MovingTowardsPlayer() { 
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
    }
}
