using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Animator animator;  
    public float attackRange = 5f;  
    public int attackDamage = 10;   
    public int health = 100;        
    public Transform player;
    public NavMeshAgent agent;
    public float chaseSpeed = 3.5f;
    public HealthBar healthBar;

    private bool isAttacking = false;
    private bool isChasing = false;
    private bool isDead = false;
    private int currentHealth;
    private PlayerHealth playerHealth;

    void Start()
    {

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
        playerHealth = player.GetComponent<PlayerHealth>();
        healthBar.SetMaxHealth(health);
        currentHealth = health;

    }

    void Update()
    {
        if (!isDead)
        {
            if (isChasing)
            {

                agent.SetDestination(player.position);


                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer <= attackRange && !isAttacking)
                {

                    StartCoroutine(AttackPlayer());
                }
            }
        }
        else
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);  

        if (currentHealth <= 0)
        {
            isDead = true;
            
        }
    }

    private IEnumerator AttackPlayer()
    {
       

        isAttacking = true;
        agent.isStopped = true;
        animator.SetBool("IsAttacking", true);
        animator.SetBool("IsWalking", false);


        yield return new WaitForSeconds(1f);


        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("MONSTRE attaque");
        }

        animator.SetBool("IsAttacking", false);
        isAttacking = false;
        agent.isStopped = false;
        animator.SetBool("IsWalking", true);
    }


    void Die()
    {
        
        animator.SetTrigger("Die");
        agent.isStopped = true;
        Destroy(gameObject, 2f);
    }

   
    private void OnTriggerEnter(Collider other)
    {
      
     
        if (other.CompareTag("Player"))
        {
            
            isChasing = true;
            animator.SetTrigger("Scream");
            animator.SetBool("IsWalking", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            isChasing = false;  
            animator.SetBool("IsWalking", false);  
        }
    }
}
