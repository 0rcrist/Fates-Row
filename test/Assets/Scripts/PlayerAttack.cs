using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerAttack : NetworkBehaviour
{
    [SerializeField]
    private float timeBtwAttacks;
    private float startAttacks;

    public Transform attackPos;
    public float attackRange;

    [SyncVar]
    private bool isAttacking = false;

    public LayerMask whatIsEnemy;

    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                performAttack();
            }
        }
        else
        {
            if (isAttacking == true)
            {
                attack();
                isAttacking = false;
            }
        }

    }
    public void performAttack()
    {
        if (timeBtwAttacks <= 0)
        {
            timeBtwAttacks = startAttacks;
            isAttacking = true;
            attack();
            timeBtwAttacks = startAttacks;
        }
        else
        {
            timeBtwAttacks -= Time.fixedDeltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<EnemyHealth>().LowerHealth(damage);

        }
    }

}