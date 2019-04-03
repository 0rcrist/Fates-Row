using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightVision : MonoBehaviour
{
    //cache
    EdgeCollider2D myCollider;
    Player thePlayer;
    Knight theKnight;
    PolygonCollider2D myPolyCollider;

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
        thePlayer = FindObjectOfType<Player>();
        myCollider = GetComponent<EdgeCollider2D>();
        myPolyCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        UpdatePosition();
        UpdateKnight();
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
        pointsHolder[1] = new Vector2(thePlayer.transform.position.x - theKnight.transform.position.x, thePlayer.transform.position.y - theKnight.transform.position.y - .5f);
        pointsHolder[2] = new Vector2(thePlayer.transform.position.x - theKnight.transform.position.x, thePlayer.transform.position.y - theKnight.transform.position.y - .4f);
        myPolyCollider.points = pointsHolder;
    }
}
