﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Frog : MonoBehaviour
{
    [Header("Frog Tuning")]
    [SerializeField] float moveSpeed = 0f;
    [Tooltip("hop height when he is not chasing player")]
    [SerializeField] float hopHeight = 2f;
    [Tooltip("hop delay when he is not chasing player")]
    [SerializeField] float hopDelay = 5f;
    [Tooltip("Range at which he will start chasing player")]
    [SerializeField] float enemySeePlayerRange = 5f;
    [Tooltip("Delay between each hop(in frames)")]
    [SerializeField] int chaseHopDelay = 7;
    [Header("If you want a random location near player")]
    [SerializeField] bool RandomNearPlayer = false;
    [SerializeField] float RandomNearRange = 2f;
    [Header("If you want frog to predict player location")]
    [SerializeField] bool PlayerPredictor = false;
    [SerializeField] bool PlayerContinuousPredictor = false;
    [SerializeField] float continuousPredictorDifficulty = 50f;
    [Header("Makse frog height always this *set to 0 if no using*")]
    [SerializeField] float hopHeightChase = 0f;
    [Header("If you want a random hop delay")]
    [SerializeField] bool randomHopDelay = false;
    [SerializeField(), Range(0f, 20f)] int RangeMax;
    [SerializeField(), Range(0f, 20f)] int RangeMin;

    //cache
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Player thePlayer;
    PolygonCollider2D feetCollider;

    bool SeePlayer = false;
    int isdirright = 1;
    bool canHop = true;
    int isJumpingToPlayer = 0;
    bool isInAir = false;
    float pastPlayerPosition;
    int setChasePredictorFrameCount = 0;
    float randomHopDelayNumber = 10f;
    float chaseSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<PolygonCollider2D>();
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        DoesHeSeePlayer();
        TouchingGroundStopX();//this has to go before hop or else one second glitch when he hops will jsut touch ground instantly after
        if(SeePlayer)
        {
            Chase();
        }
        else
        {
            Hop();
        }
    }

    private void Chase()
    {
        if(!isInAir)
        {
            HandleJumpingToPlayerCounter();
        }
        if(isJumpingToPlayer == 0)
        {
            JumpToPlayer();
        }
        if(isJumpingToPlayer == 1)
        {
            XToPlayer();
        }

    }
    private void HandleJumpingToPlayerCounter()
    {
        if (isJumpingToPlayer >= 1)
        {
            isJumpingToPlayer++;
            if (randomHopDelay)
            {
                if (isJumpingToPlayer > randomHopDelayNumber)
                {
                    isJumpingToPlayer = 0;
                }
            }
            else
            {
                if (isJumpingToPlayer > chaseHopDelay)
                {
                    isJumpingToPlayer = 0;
                }
            }
        }
    }
    private void XToPlayer()
    {
        if(PlayerContinuousPredictor)
        {
            if (setChasePredictorFrameCount == 0)
            {
                SetChaseSpeedPredictor();
            }
            setChasePredictorFrameCount++;
            if (setChasePredictorFrameCount > continuousPredictorDifficulty)
            {
                setChasePredictorFrameCount = 0;
            }
        }
    myRigidbody.velocity = new Vector2(chaseSpeed, myRigidbody.velocity.y);
        Flip();
        
    }
   private void JumpToPlayer()
    {
        myAnimator.SetTrigger("Jump");
        if(PlayerPredictor)
        {
            SetChaseSpeedPredictor();
            if(hopHeightChase != 0)
            {
                myRigidbody.velocity += new Vector2(0f, hopHeightChase);
            }
            else
            {
                SetHopVelocityPredictor();
            }
        }
        else if(RandomNearPlayer)
        {
            SetChaseSpeedRandom();
            if (hopHeightChase != 0)
            {
                myRigidbody.velocity += new Vector2(0f, hopHeightChase);
            }
            else
            {
                SetHopVelocityRandom();
            }         
        }
        else
        {
            SetChaseSpeed();
            if (hopHeightChase != 0)
            {
                myRigidbody.velocity += new Vector2(0f, hopHeightChase);
            }
            else
            {
                SetHopVelocity();
            }    
        }
        pastPlayerPosition = thePlayer.transform.position.x;
        isJumpingToPlayer = 1;
        randomHopDelayNumber = 10 * Random.Range(RangeMin, RangeMax);//1f,10f defaults
    }
    private void SetChaseSpeedRandom()
    {
        float PlayerFrogXDiff = transform.position.x - thePlayer.transform.position.x;
        float randomOffSet = Random.Range(0f, RandomNearRange);
        if (PlayerFrogXDiff == Mathf.Epsilon)
        {
            chaseSpeed = 1f;
        }
        else
        {
            if (IsPlayerInFront())
            {
                chaseSpeed = 2.7f * Mathf.Sqrt(-PlayerFrogXDiff) + randomOffSet ;
                if (Mathf.Abs(PlayerFrogXDiff) < 2)
                {
                    chaseSpeed -= randomOffSet;
                }
                if (PlayerFrogXDiff < -10)
                {
                    chaseSpeed -= 1;
                }
            }
            else
            {
                chaseSpeed = 2.7f * -Mathf.Sqrt(PlayerFrogXDiff) + randomOffSet;
                if (Mathf.Abs(PlayerFrogXDiff) < 2)
                {
                    chaseSpeed -= randomOffSet;
                }
                if (PlayerFrogXDiff > 10)
                {
                    chaseSpeed += 1;
                }
            }
        }
        Flip();
    }
    private void SetHopVelocityRandom()
    {
        float PlayerFrogXDiff = transform.position.x - thePlayer.transform.position.x;
        if (Mathf.Abs(PlayerFrogXDiff) < 2)//if really close just set speed
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(4f));
        }
        else
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(Mathf.Abs(transform.position.x - thePlayer.transform.position.x)));
        }
    }
    private void SetChaseSpeedPredictor()
    {
        float playerVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.x;
        float PlayerFrogXDiff = transform.position.x - thePlayer.transform.position.x;
        if (PlayerFrogXDiff == Mathf.Epsilon)
        {
            chaseSpeed = 1f;
        }
        else
        {
            if(IsPlayerInFront())
            {
                if (playerVelocity > 0)
                {
                    chaseSpeed = 2.7f * Mathf.Sqrt(-PlayerFrogXDiff) + playerVelocity;
                    if (PlayerFrogXDiff < -10)
                    {
                        chaseSpeed -= 1;
                    }
                }
                else if (playerVelocity == 0)
                {
                    chaseSpeed = 2.7f * Mathf.Sqrt(-PlayerFrogXDiff);
                    if (PlayerFrogXDiff < -10)
                    {
                        chaseSpeed -= 1;
                    }
                }
                else
                {
                    chaseSpeed = 2.7f * Mathf.Sqrt(-PlayerFrogXDiff) + playerVelocity;
                    if (PlayerFrogXDiff > 10)
                    {
                        chaseSpeed += 1;
                    }
                }
            }
            else
            {
                if (playerVelocity > 0)
                {
                    chaseSpeed = 2.7f * -Mathf.Sqrt(PlayerFrogXDiff) + playerVelocity;
                    if (PlayerFrogXDiff < -10)
                    {
                        chaseSpeed -= 1;
                    }
                }
                else if (playerVelocity == 0)
                {
                    chaseSpeed = 2.7f * -Mathf.Sqrt(PlayerFrogXDiff);
                    if (PlayerFrogXDiff < -10)
                    {
                        chaseSpeed -= 1;
                    }
                }
                else
                {
                    chaseSpeed = 2.7f * -Mathf.Sqrt(PlayerFrogXDiff) + playerVelocity;
                    if (PlayerFrogXDiff > 10)
                    {
                        chaseSpeed += 1;
                    }
                }
            }          
        }
    }
    private void SetHopVelocityPredictor()
    {
        float playerVelocity = thePlayer.GetComponent<Rigidbody2D>().velocity.x;
        float PlayerFrogXDiff = (transform.position.x - thePlayer.transform.position.x);
        if (Mathf.Abs(PlayerFrogXDiff) < 2)//if really close just set speed
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(4f));
        }
        else
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(Mathf.Abs(transform.position.x - thePlayer.transform.position.x)));
        }
    }
    private void SetHopVelocity()
    {
        float PlayerFrogXDiff = transform.position.x - thePlayer.transform.position.x;
        if (Mathf.Abs(PlayerFrogXDiff) < 2)//if really close just set speed
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(4f));
        }
        else
        {
            myRigidbody.velocity += new Vector2(0f, 6 * Mathf.Sqrt(Mathf.Abs(transform.position.x - thePlayer.transform.position.x)));
        }
    }
    private void SetChaseSpeed()
    {
        float PlayerFrogXDiff = transform.position.x - thePlayer.transform.position.x;
        if (PlayerFrogXDiff == Mathf.Epsilon)
        {
            chaseSpeed = 1f;
        }
        else
        {
            if (IsPlayerInFront())
            {
                chaseSpeed = 2.7f * Mathf.Sqrt(-PlayerFrogXDiff);
                if (PlayerFrogXDiff < -10)
                {
                    chaseSpeed -= 1;
                }
            }
            else
            {
                chaseSpeed = 2.7f * -Mathf.Sqrt(PlayerFrogXDiff);
                if (PlayerFrogXDiff > 10)
                {
                    chaseSpeed += 1;
                }
            }           
        }
        Flip();
    }
    private bool IsTouchingGround()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("ForegroundTile","Player")))
        {
            return true;
        }
        return false;
    }

    private void DoesHeSeePlayer()
    {
        float EnemyPlayerXDifference = transform.position.x - thePlayer.transform.position.x;
        if (Mathf.Abs(EnemyPlayerXDifference) < enemySeePlayerRange)
        {
            SeePlayer = true;
        }
    }

    private void Flip()
    {
        if(SeePlayer)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);//-(Mathf.Sign(myRigidbody.velocity.x))   
        }
        else
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
        }    
    }

    private void TouchingGroundStopX()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("ForegroundTile","Player")))
        {
            isInAir = false;
            if(isJumpingToPlayer == 1)//bug this gets called when he hops one frame ahead
            {
                return;
            }
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
        else
        {
            isInAir = true;
        }
    }

    private void Hop()
    {
        if(canHop == true)
        {
            StartCoroutine(HopRoutine());
        }
    }

    IEnumerator HopRoutine()
    {
        float random = Random.Range(-1f, 1f);
        if(random > 0) { myRigidbody.velocity = new Vector2(1* moveSpeed, hopHeight); }
        else { myRigidbody.velocity = new Vector2(-1 * moveSpeed, hopHeight); }
        Flip();
        myAnimator.SetTrigger("Jump");
        canHop = false;
        yield return new WaitForSeconds(hopDelay);
        canHop = true;
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
    private bool IsPlayerInFrontChase()
    {
        float EnemyPlayerXDifference = transform.position.x - pastPlayerPosition;
        if (EnemyPlayerXDifference < 0)//player is in front
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}