﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float enemySeePlayerRange = 5f;

    bool canSeePlayer = false;
    int framestilljumpagain = 0;//to fix double jump bug
    bool isAttacking = false;
    bool inAttackCoRoutine = false;
    //cache
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    PolygonCollider2D myFeetCollider;
    Player thePlayer;
   

    //run up to player stops and swings, repeat
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSeePlayer)
        {
            IsHeAttacking();
            if(isAttacking)
            {
                if(!inAttackCoRoutine)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                Chase();
            }
        }
        else
        {
            Roam();
        }
        if (framestilljumpagain > 0)
        {
            framestilljumpagain++;
            if (framestilljumpagain == 5)
            {
                framestilljumpagain = 0;
            }
        }
    }

    IEnumerator Attack()
    {
        inAttackCoRoutine = true;
        myRigidBody.velocity = new Vector2(0f, 0f);
        myAnimator.SetBool("Walk", false);
        myAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        inAttackCoRoutine = false;
    }

    private void IsHeAttacking()
    {
        if (Mathf.Abs(transform.position.x - thePlayer.transform.position.x) < 1f && Mathf.Abs(transform.position.y - thePlayer.transform.position.y) < 3f)
        {
            isAttacking = true;
        }
    }

    private void Roam()
    {
        myAnimator.SetBool("Walk", false);
        myRigidBody.velocity = new Vector2(0f, 0f);
    }

    private void Chase()
    {
        myAnimator.SetBool("Walk", true);
        Flip();
        if (IsPlayerInFront())//player is in front
        {
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        }
        else if (!IsPlayerInFront())//player behind
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
        }
    }

    private void Flip()
    {
        if(canSeePlayer)
        {
            if(IsPlayerInFront())
            {
                transform.localScale = new Vector2(-1f, 1f);
                GetComponentInChildren<KnightVision>().transform.localScale = new Vector3(-1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(1f, 1f);
                GetComponentInChildren<KnightVision>().transform.localScale = new Vector3(1f, 1f);
            }
        }
    }

    private bool IsPlayerInFront()
    {
        float EnemyPlayerXDifference = transform.position.x - thePlayer.transform.position.x;
        if (EnemyPlayerXDifference < 0)//player is in front
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SeesPlayer(bool x)
    {
        if(x == false)
        {
            if(Mathf.Abs(transform.position.x - thePlayer.transform.position.x) < enemySeePlayerRange)
            {
                //canSeePlayer = true;
                return;
            }
        }
        canSeePlayer = x;
    }
    public void JumpKnight()
    {
        if(isAttacking)
        {
            return;
        }
        if (!canSeePlayer)
        {
            return;
        }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("ForegroundTile")))
        {
            return;
        }
        if (framestilljumpagain != 0)
        {
            return;
        }
        if (!IsPlayerInFront() && myRigidBody.velocity.x > 0)//rat is in front and velocity positive
        {
            return;
        }
        if (IsPlayerInFront() && myRigidBody.velocity.x < 0)//rat is in back and velocity negatives
        {
            return;
        }
        Vector2 jumpVector = new Vector2(0f, jumpHeight);
        myRigidBody.velocity += jumpVector;
        framestilljumpagain++;
    }
}
