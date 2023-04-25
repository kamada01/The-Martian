using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float range;
    [SerializeField] private float attackCD;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;

    private float cdTimer = Mathf.Infinity;
    private float HP;

    Animator animator;
    private Rigidbody2D rb;
    private AlphaVisionController visionController;
    private Vector2 targetDirection;
    private RaycastHit2D hit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        visionController = GetComponent<AlphaVisionController>();
        HP = 3;

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
                Debug.Log("Launching attack");

                animator.SetTrigger("attack");
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    //Reduce Player's HP in a sub
                    //Debug.Log("Attack causes damage");
                }
            }
        }
        else
        {
            MovingTowardsPlayer();
        }

    }

    private bool PlayerWithinAttackRange()
    {
        //create a box collider representing the attack range 
        hit = Physics2D.BoxCast(boxCollider.bounds.center +
            transform.right * range * transform.localScale.x*1.3f+Vector3.down*0.7f,
            boxCollider.bounds.size*0.6f, 0, Vector2.left, 0);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Player within attack range.");
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
            transform.right * range * transform.localScale.x * 1.3f + Vector3.down * 0.7f,
            boxCollider.bounds.size * 0.6f);
    }

    private void DamagePlayer()
    {
        //To be implement after setting up the player's hp codes
    }

    private void TakingDamage()
    {
        //To be implement after settig up the player's weapons
    }

    private void MovingTowardsPlayer() { 
        if (visionController.AwareOfPlayer)
        {
            targetDirection = visionController.DirectionToPlayer;
            animator.SetTrigger("move");
            Debug.Log("Player detected. Moving towards the player");

            //Flip the spirte if necessary
            if (targetDirection.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (targetDirection.x < 0)
            {
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.AddForce(targetDirection * speed * Time.deltaTime);

        }
        else
        {
            targetDirection = Vector2.zero;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("idle");
            Debug.Log("Player Not detected.");
        }

    }

}
