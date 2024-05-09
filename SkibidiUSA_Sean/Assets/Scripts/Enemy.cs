using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
   [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    //hides parts of an object in this case parts of player and parts of ground
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private float health;
    //patrol
    [SerializeField] private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;
    //attacks
    [SerializeField] private float timeBetweenAttacks = 5.0f;
    bool alreadyAttacked;
    //states
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool isDead = false;
    private void Awake()
    {
        player = GameObject.Find("PlayerSniper").transform;
        agent.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (isDead) Destroy(gameObject);
    }
 
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        }
    }
  private void ResetAttack()
    {
        alreadyAttacked=false;
    }

        

/*private void OnDrawGizmosSeleted()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    } */
    //can't serialize this  tried
   public void TakeDamage( int damage)
    {
        health -= damage;
        if ( health <0)
        {
            isDead = true;
        }
    }
}

