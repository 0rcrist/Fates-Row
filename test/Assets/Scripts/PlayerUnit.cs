using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false) {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") *runSpeed;

    }

    void FixedUpdate() {
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, false);
    }
}
