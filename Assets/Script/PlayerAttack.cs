using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public Animator animator;
    public float aS = 1f;

    private float cooldown = 0f;
    

    void Start()
    {

        animator = GetComponent<Animator>();
        
    }

    
    void Update()
    {

        if (Time.time >= cooldown && Input.GetKeyDown(KeyCode.E))
        {
            Attack();
            cooldown = Time.time +1 / aS;
        }

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);


        foreach (Collider enemy in hitEnemies)
        { 

            Monster monster = enemy.GetComponent<Monster>();

            if (monster != null)
            {

                monster.TakeDamage(attackDamage);
                

            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
