using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBeast : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float roamRange = 10f;

    int isdirright = 1;
    float initialposition;

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
        CheckIfEnemyIsWithinRange();
        Roam();
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
