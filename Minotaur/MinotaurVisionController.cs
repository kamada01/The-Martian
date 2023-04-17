using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurVisionController : MonoBehaviour
{
    public bool AwareOfPlayer {   get;        private set;    }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float vision;

    [SerializeField]
    private Transform player;

    private void Awake()
    {
        //_player = FindObjectOfType<PlayerMovement>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        //DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= vision)
        {
            DirectionToPlayer = enemyToPlayerVector.normalized;
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
        
    }
}
