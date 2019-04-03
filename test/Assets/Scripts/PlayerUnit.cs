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

 

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }

        if (joy.Horizontal > 0f)
        {
            horizontalMove = runSpeed;
        }
        else if (joy.Horizontal < 0f)
        {
            horizontalMove = -runSpeed;
        }
        else {
            horizontalMove = 0f;
        }
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
