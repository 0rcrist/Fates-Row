using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 150f;
    bool jump = false;
    public Joystick joy;
    public Animator ani;

    

    

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }
        ani.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (joy.Horizontal > 0f)
        {
            horizontalMove = runSpeed;
            // ani.SetFloat("Speed", horizontalMove);
        }
        else if (joy.Horizontal < 0f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;

        }
         
        Debug.Log("speed is " + ani.GetFloat("Speed"));
        }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void isJumping() {
        jump = true;
    }

}
