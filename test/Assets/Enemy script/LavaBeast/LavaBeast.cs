﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBeast : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float roamRange = 10f;
    [SerializeField] flameground theGroundFire;
    [SerializeField] int groundfireMoveSpeed = 10;
    [SerializeField] FireBall velocitygivenBall;
    [SerializeField] FireBall nogravityBall;
    [SerializeField] FireBall cosineBall;
    [SerializeField] FireBall immobileBall;
    [SerializeField] FireBall straightball;

    int isdirright = 1;
    float initialposition;
    //deal with phases
    int phasecounter = 1;
    bool lavagroundphase = true;
    bool burstattackphase = false;
    bool laserbeamphase = false;
    bool cosinephase = false;
    int switchphasecounter = 1;

    //cosine attack
    bool cosineattackagain = true;

    //laserbeam attack
    bool laserattackagain = true;
    float laseroffset = 0;
    bool laserswitch = false;
    float laserbeamangle = 0;
    float laserradius = 5;
    float anglechange = 4f;

    //lavaground attack
    bool flameground1 = false;
    bool flameground2 = true;
    bool attackAgain = true;
    float groundfiredelay = 1f;//.05 is good for compact line

    //burst attack
    bool burstattackAgain = true;
    float burstfiredelay = 2f;

    Animator myAnimator;
    Player thePlayer;
    Rigidbody2D myRigidBody;

    //laser beam like from boshy tekken boss
    //lava shoots out around him circular 
    //has bomb lava that he throws and breaks into more
    //lava balls that fly in any direction
    //rain of lava
    //charge attack
    //line of lava on the floor you have to jump to avoid

    //**punches ground and lava balls fly ina outward circle
    // Start is called before the first frame update
    void Start()
    {
        initialposition = transform.position.x;
        myRigidBody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        DealWithPhases();
        //CheckIfEnemyIsWithinRange();
        //Roam();
        if(burstattackphase)
        {
            if (burstattackAgain)
            {
                burstattackAgain = false;
                BurstAttack();
            }
        }
        if(lavagroundphase)
        {
            if (attackAgain)
            {
                attackAgain = false;
                LavaGroundAttack();
            }
        }
        if(cosinephase)
        {
            if(cosineattackagain)
            {
                cosineattackagain = false;
                StartCoroutine(Cosineattack());
            }
        }
        if(laserbeamphase)
        {
            if (laserattackagain == true)
            {
                laserattackagain = false;
                StartCoroutine(LaserBeamAttack());
            }

        }
        
    }

    private void DealWithPhases()
    {
        /*bool lavagroundphase = true;
        bool burstattackphase = false;
        bool laserbeamphase = false;
        bool cosinephase = false;*/

        if (switchphasecounter % 1000 == 0)
        {
            if(phasecounter == 1)
            {
                lavagroundphase = false;
                burstattackphase = true;
            }
            if (phasecounter == 2)
            {
                burstattackphase = false;
                cosinephase = true;
            }
            if (phasecounter == 3)
            {
                cosinephase = false;
                laserbeamphase = true;
            }
            if (phasecounter == 4)
            {
                laserbeamphase = false;
                lavagroundphase = true;
            }
            phasecounter++;
            if(phasecounter > 4)
            {
                phasecounter = 1;
            }
        }
        switchphasecounter++;
    }

    private void BurstAttack()
    {
        StartCoroutine(BurstSpawn());
    }
    IEnumerator LaserBeamAttack()
    {
        laseroffset = Mathf.Abs(-laserradius * Mathf.Cos(((Mathf.PI * laserbeamangle) / 180) - Mathf.PI / 2)) + 2;//-cos(x-pi/2) + 2
        FireBall ballupper = Instantiate(nogravityBall, new Vector2(transform.position.x-2f, transform.position.y+3), Quaternion.identity) as FireBall;
        ballupper.setVelocity(-17f, 0);
        ballupper.setoffest(laseroffset);
        FireBall ball = Instantiate(nogravityBall, new Vector2(transform.position.x-2f, transform.position.y+2), Quaternion.identity) as FireBall;
        ball.setVelocity(-17f,0);
        ball.setoffest(-laseroffset);
        yield return new WaitForSeconds(.01f);//.01f
        if(laserbeamangle > 180)
        {
            laserradius = Random.Range(1f, 6f);
            anglechange = Random.Range(4, 8);
            laserbeamangle = 0;
        }
        laserbeamangle+= anglechange;
   
        laserattackagain = true;
    }
    IEnumerator Cosineattack()
    {
        myAnimator.SetTrigger("skill_1");
        FireBall ball = Instantiate(cosineBall, new Vector2(transform.position.x,transform.position.y + 1), Quaternion.identity) as FireBall;
        ball.setMoveSpeed(Random.Range(5f,10f));
        ball.setDesiredRadius(Random.Range(1f, 10f));
        ball.setthetalower(Random.Range(0,2));
        yield return new WaitForSeconds(.5f);
        cosineattackagain = true;
    }
    IEnumerator BurstSpawn()
    {
        //settings 1 is gravity scale 2, x(-10,10),y(15,30) spawning 30 makes them go high and shoot down fast
        //settings gravity scale 1, x(-10,10),y(10,15) spawning 30 very symetric regular burst
        myAnimator.SetTrigger("jump");
        yield return new WaitForSeconds(.8f);
        for (int i = 0; i < 30; i++)
        {
            FireBall ball = Instantiate(velocitygivenBall, transform.position, Quaternion.identity) as FireBall;
            float x = Random.Range(-13f,13f);//-10f,10f
            float y = Random.Range(15f, 30f);//10f,15f
            ball.setVelocity(x,y);
        }
        yield return new WaitForSeconds(burstfiredelay);
        burstattackAgain = true;
    }
    private void FacePlayer()
    {
        if(thePlayer.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-.02f, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(.02f, transform.localScale.y);
        }
    }

    private void LavaGroundAttack()
    {
        StartCoroutine(FireGround());
    }
    IEnumerator FireGround()
    {
        myAnimator.SetTrigger("skill_1");
        FireBall ball = Instantiate(velocitygivenBall, transform.position, Quaternion.identity) as FireBall;
        float x = Random.Range(-10f,-5f);//-10f,10f
        float y = Random.Range(10f, 15f);//10f,15f
        ball.setVelocity(x, y);
        flameground flame = Instantiate(theGroundFire, new Vector2(transform.position.x,transform.position.y + .3f), Quaternion.identity) as flameground;
        if(transform.localScale.x < 0)
        {
            flame.setFlameMoveSpeed(-groundfireMoveSpeed);
        }
        else
        {
            flame.setFlameMoveSpeed(groundfireMoveSpeed);
        }
        if(flameground1)
        {
            if (Random.Range(-70, 30) < 0)
            {
                groundfiredelay = .05f;
            }
            else
            {
                groundfiredelay = 1f;
            }
        }
        else if(flameground2)
        {
            //40 percent quick 30 percent long 30 percent mediuem
            int i = Random.Range(0,100);
            if (i < 50)
            {
                groundfiredelay = .05f;
            }
            else if(i > 50 && i < 80)
            {
                groundfiredelay = .5f;
            }
            else
            {
                groundfiredelay = 1f;
            }
        }
        yield return new WaitForSeconds(groundfiredelay);
        attackAgain = true;
    }
    private void CheckIfEnemyIsWithinRange()
    {
        if (transform.position.x > initialposition + roamRange || transform.position.x < initialposition - roamRange)
        {
            ChildChangeisdirrightLava();
        }
    }
    public void ChildChangeisdirrightLava()
    {
        isdirright = isdirright * -1;
    }
    private void Roam()
    {
        if (isdirright == 1)
        {    
                transform.localScale = new Vector2(.02f, transform.localScale.y);
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y); 
        }
        else
        {
            transform.localScale = new Vector2(-.02f, transform.localScale.y);
            myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
        }
    }
}