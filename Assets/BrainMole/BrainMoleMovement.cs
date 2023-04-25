using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMoleMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private int damage;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private float vision;
    [SerializeField] private Transform player;

    private Vector2 DirectionToPlayer;

    private float cdTimer = Mathf.Infinity;
    private float HP;

    Animator animator;
    private Rigidbody2D rb;
    //private BrainMoleVisionController visionController;
    //private Vector2 targetDirection;
    private RaycastHit2D hit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //visionController = GetComponent<BrainMoleVisionController>();
        HP = 1;    

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
        // Wait for 0.5 second 
        yield return new WaitForSeconds(0.5f);
        // only reduce player's HP if the player is still within the attacking range after 1 sec
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Reduce Player's HP in a sub
            //DamagePlayer(damage);
            //debug message
            //Debug.Log("Attack causes damage");
        }
    }


    private bool PlayerWithinAttackRange()
    {
        //create a box collider used for attack detection
        hit = Physics2D.BoxCast(capsuleCollider.bounds.center + 
            transform.right * attackRange * transform.localScale.x,
            capsuleCollider.bounds.size*0.5f, 0, Vector2.left, 0);

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
        Gizmos.DrawWireCube(capsuleCollider.bounds.center +
            transform.right * attackRange * transform.localScale.x,
            capsuleCollider.bounds.size* 0.5f);
    }

    private void DamagePlayer()
    {
        //To be implement after setting up the player's hp codes
    }

    public void TakingDamage(float damageTaken)
    {
        //To be implement after settig up the player's weapons
        HP = HP - damageTaken;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MovingTowardsPlayer() {
        if (AwareOfPlayer())
        {
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


}
