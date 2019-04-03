using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    float velX;
    float velY;
    float playerX;
    float playerY;
    float arrowX;
    float arrowY;
    bool straight = false;
    bool curvy = false;
    bool accelerating = false;
    //straight fireball, curvy fireball, accelerating fireball,
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        playerX = FindObjectOfType<Player>().transform.position.x;
        playerY = FindObjectOfType<Player>().transform.position.y;
        arrowX = transform.position.x;
        arrowY = transform.position.y;

        if(straight)
        {
            velX = -5;
            velY = -1;
        }
        if(curvy)
        {

        }
        if(accelerating)
        {

        }
         myRigidBody.velocity = new Vector2(velX, velY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setStraight()
    {
        straight = true;
    }
    public void setCurvy()
    {
        curvy = true;
    }
    public void setAccelerating()
    {
        accelerating = true;
    }
}
