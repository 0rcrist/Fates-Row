using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] bool isMage = false;
    [SerializeField] FireBall thefireball;
    [SerializeField] float fireBallMoveSpeed = 10f;
    [SerializeField] float fireBallShootSpeed = 1f;
    [SerializeField] float FireBallChargeUpTime = 1f;
    [SerializeField] bool straightBall = false;
    [SerializeField] bool curvyBall = false;
    [SerializeField] bool acceleratingBall = false;
    bool canSeePlayer = false;
    [Header("*To change arrow speed go to arrow prefab*")]
    [SerializeField] Arrow theArrow;
    [SerializeField] bool gasArrow;
    [Tooltip("Time between each shot in seconds")]
    [SerializeField] float shootSpeed = 1f;
    [Tooltip("Time for him to charge attack in seconds")]
    [SerializeField] float chargeUpTime = 1f;
    [SerializeField] float moveSpeed = 1f;
    bool shootingisrunning = false;
    int isdirright = 1;

    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSeePlayer)
        {
            myRigidBody.velocity = new Vector2(0f, 0f);
            GetComponent<Animator>().SetBool("Walk", false);
            Attack();
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            Roam();
        }

        
    }

    private void Roam()
    {
        if (isdirright == 1)
        {
            GetComponentInChildren<WallSwitchColliderArcher>().transform.localScale = new Vector3(1 * transform.localScale.x, transform.localScale.y);
            GetComponentInChildren<WallSwitchColliderArcher>().transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = true;
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        }
        else
        {
            GetComponentInChildren<WallSwitchColliderArcher>().transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
            GetComponentInChildren<WallSwitchColliderArcher>().transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = false;
            myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
        }
    }
    public void ChildChangeisdirrightArcher()
    {
        isdirright = isdirright * -1;
    }
    private void Attack()
    {
        Flip();
        if(!shootingisrunning)
        {
            shootingisrunning = true;
            if(isMage)
            {
                StartCoroutine(ShootingMage());
            }
            else
            {
                StartCoroutine(Shooting());
            }
        }
    }

    private void Flip()
    {
        if(FindObjectOfType<Player>().transform.position.x - transform.position.x > 0)//to your right
        {
            GetComponent<SpriteRenderer>().flipX = true;
            return;
        }
        //to your left
        GetComponent<SpriteRenderer>().flipX = false;
    }

    IEnumerator Shooting()
    {
        GetComponent<Animator>().SetBool("Shoot",true);
        yield return new WaitForSeconds(chargeUpTime);
        GetComponent<Animator>().SetBool("Shoot", false);
        Arrow arrow = Instantiate(theArrow, transform.position, Quaternion.identity) as Arrow;
        if(gasArrow == true)
        {
            //change color
            arrow.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            //change arrow gasboolean
            arrow.setGasOn();
        }
        yield return new WaitForSeconds(shootSpeed);
        shootingisrunning = false;

    }
    IEnumerator ShootingMage()
    {
        yield return new WaitForSeconds(FireBallChargeUpTime);
        FireBall fireball = Instantiate(thefireball, transform.position, Quaternion.identity) as FireBall;
        fireball.setMoveSpeed(fireBallMoveSpeed);
        if(straightBall)
        {
            fireball.setStraight();
        }
        if(curvyBall)
        {
            fireball.setCurvy();
        }
        if(acceleratingBall)
        {
            fireball.setAccelerating();
        }
        yield return new WaitForSeconds(fireBallShootSpeed);
        shootingisrunning = false;

    }
    public void SeesPlayer(bool x)
    {
        canSeePlayer = x;
    }
}
