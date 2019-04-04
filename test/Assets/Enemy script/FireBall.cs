using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] bool curvycosine = false;
    [SerializeField] bool curvydefault = false;

    float moveSpeed = 10f;
    int DeathCounter = 0;
    float velX;
    float velY;
    float playerX;
    float playerY;
    float BallX;
    float BallY;
    bool straight = false;
    bool curvy = false;
    float radius;
    bool thetalower = true;
    float theta = 0;
    float originaltheta = 0;
    bool accelerating = false;
    //straight fireball, curvy fireball, accelerating fireball,
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        playerX = FindObjectOfType<Player>().transform.position.x;
        playerY = FindObjectOfType<Player>().transform.position.y;
        BallX = transform.position.x;
        BallY = transform.position.y;

        if (straight)
        {
            if (Mathf.Abs(playerX - BallX) > Mathf.Abs(playerY - BallY))
            {
                //calculates time needed to get to distance. movespeed is 10 so 10 units a second if distance is 20 it would take 2 seconds so t is 2
                //equation x = x0 + v0t + 0 (no acceleration on x)
                //equation y = y0 + v0t  + 0 (no acceleration on y either)

                float xtime = Mathf.Abs(playerX - BallX) / moveSpeed;
                velX = (playerX - BallX) / xtime;
                velY = (playerY - BallY) / xtime;
            }
            else
            {
                float ytime = Mathf.Abs(playerY - BallY) / moveSpeed;
                velY = (playerY - BallY) / ytime;
                velX = (playerX - BallX) / ytime;
            }
            myRigidBody.velocity = new Vector2(velX, velY);
        }
        if (curvy)
        {
            if (Mathf.Abs(playerX - BallX) > Mathf.Abs(playerY - BallY))
            {
                //calculates time needed to get to distance. movespeed is 10 so 10 units a second if distance is 20 it would take 2 seconds so t is 2
                //equation x = x0 + v0t + 0 (no acceleration on x)
                //equation y = y0 + v0t  + 0 (no acceleration on y either)

                float xtime = Mathf.Abs(playerX - BallX) / moveSpeed;
                velX = (playerX - BallX) / xtime;
                velY = (playerY - BallY) / xtime;
            }
            else
            {
                float ytime = Mathf.Abs(playerY - BallY) / moveSpeed;
                velY = (playerY - BallY) / ytime;
                velX = (playerX - BallX) / ytime;
            }
            //here radius is based off initial speed so bigger radius bigger speed / radius = Mathf.Sqrt(Mathf.Pow(velX, 2) + Mathf.Pow(velY, 2));
            //radius = Mathf.Sqrt(Mathf.Pow(velX, 2) + Mathf.Pow(velY, 2));
            radius = 5f;
            if (velX < 0)
            {
                //radius = radius * -1;
            }
            theta = Mathf.Acos(velX / (Mathf.Sqrt(Mathf.Pow(velX, 2) + Mathf.Pow(velY, 2))));
            theta = (theta * 180) / Mathf.PI;
            if (velY < 0)
            {
                theta = (180 - theta) + 180;
            }
            originaltheta = theta;
            myRigidBody.velocity = new Vector2(velX, velY);
            //dont do anything right away
        }
        if (accelerating)
        {
            //dont do anything right away
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (straight)
        {
            //nothing to do continuously
        }
        if (curvy)
        {
            Curve();
        }
        if (accelerating)
        {

        }

        DeathCounter++;
        if (DeathCounter > 400)
        {
            Destroy(gameObject);
        }
    }

    private void Curve()
    {
        //another derivative is -x/sqrt(1-x^2)
        //x^2 + y^2 = r^2         derivative is dy/dx = -x/y

        velX = radius * Mathf.Cos((Mathf.PI * theta) / 180);
        velY = radius * Mathf.Sin((Mathf.PI * theta) / 180);
        myRigidBody.velocity = new Vector2(velX, velY);
        Debug.Log(theta);
        if (curvycosine)
        {
            if (thetalower)
            {
                theta--;
                if (theta < originaltheta - 45)
                {
                    thetalower = false;
                    // theta = 0;
                }
            }
            else
            {
                theta++;
                if (theta > originaltheta + 60)
                {
                    thetalower = true;
                    // theta = 0;
                }
            }
        }
        else if (curvydefault)
        {
            if (thetalower)
            {
                theta--;
                if (theta < originaltheta - 45)
                {
                    thetalower = false;
                    theta = originaltheta;
                }
            }
            else
            {
                theta++;
                if (theta > originaltheta + 60)
                {
                    thetalower = true;
                    theta = originaltheta;
                }
            }
        }

    }



    public void setMoveSpeed(float ms)
    {
        moveSpeed = ms;
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
