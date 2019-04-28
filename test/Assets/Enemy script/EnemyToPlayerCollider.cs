﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToPlayerCollider : MonoBehaviour
{
    static float collideDelay = 2f;//if enemy hits player he has an invincibility period..
    static bool Damage = true;//make static if I want it to follow for all enemies
    private void OnTriggerEnter2D(Collider2D collision)//can only hit players probably dont have to check name
    {
        if (Damage)
        {
            if (collision.name == "PlayerUnit(Clone)")
            {
                //DamagePlayer();
                Debug.Log("enemycollision"); 
                Damage = false;
                StartCoroutine(DelayAttack());
            }
        }
    }
    IEnumerator DelayAttack()//ifobjects get destroyed it never finishes
    {
        yield return new WaitForSeconds(collideDelay);
        Damage = true;
    }
}
