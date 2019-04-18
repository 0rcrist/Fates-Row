using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherVision : MonoBehaviour
{
    //cache
    EdgeCollider2D myCollider;
    //GameObject thePlayer;
    Archer theArcher;
    PolygonCollider2D myPolyCollider;
    GameObject[] Players;
    bool getplayersonce = true;

    bool trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        trigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        theArcher = transform.parent.GetComponent<Archer>();
        //thePlayer = GameObject.FindGameObjectWithTag("Player");
        myCollider = GetComponent<EdgeCollider2D>();
        myPolyCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getplayersonce)
        {
            getplayers();
        }
        else
        {
            UpdatePosition();
            UpdateArcher();
        }
    }
    private void getplayers()
    {
        //int counter = 0;
        Players = GameObject.FindGameObjectsWithTag("Player");
        if (Players.Length == 0)
        {

        }
        else
        {
            getplayersonce = false;
        }
    }
        private void UpdateArcher()
    {
       if(trigger == true)
        {
            transform.parent.GetComponent<Archer>().SeesPlayer(false);
        }
       else
        {
            transform.parent.GetComponent<Archer>().SeesPlayer(true);
        }
    }

    private void UpdatePosition()
    {
        Vector2[] pointsHolder;
        pointsHolder = myPolyCollider.points;
        pointsHolder[0] = new Vector2(theArcher.transform.position.x - theArcher.transform.position.x, theArcher.transform.position.y- theArcher.transform.position.y + .4f);
        pointsHolder[1] = new Vector2(Players[0].transform.position.x - theArcher.transform.position.x, Players[0].transform.position.y - theArcher.transform.position.y - .5f);
        pointsHolder[2] = new Vector2(Players[0].transform.position.x - theArcher.transform.position.x, Players[0].transform.position.y - theArcher.transform.position.y - .4f);
        myPolyCollider.points = pointsHolder;

        /*Vector2[] pointsHolder;
        pointsHolder = myCollider.points;
        pointsHolder[0] = new Vector2(theArcher.transform.position.x - theArcher.transform.position.x, theArcher.transform.position.y- theArcher.transform.position.y + .4f);
        pointsHolder[1] = new Vector2(thePlayer.transform.position.x - theArcher.transform.position.x, thePlayer.transform.position.y - theArcher.transform.position.y - .5f); 
        myCollider.points = pointsHolder;*/
    }


}
