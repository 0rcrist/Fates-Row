using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    //can roam a certain area back and forth and he can also jump when needed

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float roamRange = 10f;
    [SerializeField] float enemySeePlayerRange = 5f;
    [SerializeField] bool enemyswitchonwall = false;

    int isdirright = 1;
    int framestilljumpagain = 0;//to fix double jump bug
    float initialposition;

    Animator myAnimator;
    Rigidbody2D myRigidBody;
    PolygonCollider2D myFeetCollider;
    Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        initialposition = transform.position.x;
        myRigidBody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyIsWithinRange();
        Roam();
        if (framestilljumpagain > 0)
        {
            framestilljumpagain++;
            if (framestilljumpagain == 5)
            {
                framestilljumpagain = 0;
            }
        }
    }

    public bool GetEnemyHugEdgeState()
    {
        return enemyswitchonwall;
    }
    public void ChildChangeisdirrightSkeleton()
    {
        isdirright = isdirright * -1;
    }
    private void CheckIfEnemyIsWithinRange()
    {
        if(transform.position.x > initialposition + roamRange || transform.position.x < initialposition - roamRange)
        {
            ChildChangeisdirrightSkeleton();
        }
    }
    private void Roam()
    {
        if (isdirright == 1)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
        }
    }
    public void JumpSkeleton()
    {

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

}
