using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : MonoBehaviour
{
    [Header("Bottle Tuning *bottle speed is on bottle prefab*")]
    [Tooltip("offset from center of parabola, higher number higher throw")]
    [SerializeField, Range(-20, 20)] float xoffsetRangeMax;
    [Tooltip("offset from center of parabola, higher number higher throw")]
    [SerializeField, Range(-20, 20)] float xoffsetRangeMin;
    [Tooltip("smaller a wider throw is (a*x^2)(-.1 good base)")]
    [SerializeField, Range(-3,0)] float a;
    [Header("Jester tuning")]
    [SerializeField] float enemySeePlayerRange = 20f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float throwSpeed = 2f;
    [SerializeField] GameObject theBottle;

    Player thePlayer;
    Animator myAnimator;
    Rigidbody2D myRigidBody;

    float xoffset = 0f;
    bool seePlayer = false;
    bool isThrowing = false;
    int isdirright = 1;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DoesHeSeePlayer();
        if (seePlayer)
        {
            myRigidBody.velocity = new Vector2(0f,0f);
            myAnimator.SetBool("Throw", true);
            myAnimator.SetBool("Walk", false);
            Attack();
        }
        else
        {
            myAnimator.SetBool("Throw", false);
            myAnimator.SetBool("Walk", true);
            Roam();
        }
    }

    private void Roam()
    {
        if (isdirright == 1)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        }
        else
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
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
        if(!isThrowing)
        {
            isThrowing = true;
            StartCoroutine(Throwing());
        }
        
    }
    private void Flip()
    {
        if(IsPlayerInFront())
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
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
    IEnumerator Throwing()
    {
        //throw bottle
        xoffset = Random.Range(xoffsetRangeMin, xoffsetRangeMax);//10f
        GameObject newBottle = Instantiate(theBottle, transform.position, Quaternion.identity) as GameObject;
        newBottle.transform.parent = transform;
        yield return new WaitForSeconds(throwSpeed);//2f
        isThrowing = false;
    }
    public float getXoffset()
    {
        return xoffset;
    }
    public float getA()
    {
        return a;
    }
    private void DoesHeSeePlayer()
    {
        float EnemyPlayerXDifference = transform.position.x - thePlayer.transform.position.x;
        if (Mathf.Abs(EnemyPlayerXDifference) < enemySeePlayerRange)
        {
            seePlayer = true;
        }
    }
}
