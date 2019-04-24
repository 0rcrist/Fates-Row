using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightVision : MonoBehaviour
{
    //cache
    EdgeCollider2D myCollider;
   // Player thePlayer;
    Knight theKnight;
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
        theKnight = transform.parent.GetComponent<Knight>();
       // thePlayer = FindObjectOfType<Player>();
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
            UpdateKnight();
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
    private void UpdateKnight()
    {
        if (trigger == true)
        {
            transform.parent.GetComponent<Knight>().SeesPlayer(false);
        }
        else
        {
            transform.parent.GetComponent<Knight>().SeesPlayer(true);
        }
    }

    private void UpdatePosition()
    {
        Vector2[] pointsHolder;
        pointsHolder = myPolyCollider.points;
        pointsHolder[0] = new Vector2(theKnight.transform.position.x - theKnight.transform.position.x, theKnight.transform.position.y - theKnight.transform.position.y + .4f);
        pointsHolder[1] = new Vector2(Players[0].transform.position.x - theKnight.transform.position.x, Players[0].transform.position.y - theKnight.transform.position.y - .5f);
        pointsHolder[2] = new Vector2(Players[0].transform.position.x - theKnight.transform.position.x, Players[0].transform.position.y - theKnight.transform.position.y - .4f);
        myPolyCollider.points = pointsHolder;
    }
}
