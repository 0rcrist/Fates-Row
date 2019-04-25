using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float timeBtwAttacks;
    private float startAttacks;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatIsEnemy;

    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            performAttack();
        }
    }
    public void performAttack() {
        if (timeBtwAttacks <= 0)
        {
            timeBtwAttacks = startAttacks;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            foreach (Collider2D enemy in enemiesToDamage) {
                enemy.GetComponent<EnemyHealth>().LowerHealth(2);
            }
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
}
