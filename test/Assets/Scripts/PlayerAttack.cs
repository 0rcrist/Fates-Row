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
        
    }
    public void performAttack() {
        if (timeBtwAttacks <= 0)
        {
            timeBtwAttacks = startAttacks;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            foreach (Collider2D enemy in enemiesToDamage) {
               //enemy.GetComponent<>
            }
        }
        else
        {
            timeBtwAttacks -= Time.fixedDeltaTime;
        }
    }
}
