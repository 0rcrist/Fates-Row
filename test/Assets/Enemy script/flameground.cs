using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameground : MonoBehaviour
{
    [SerializeField] int moveSpeed = 10;
    int deathCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed *Time.deltaTime, transform.position.y);
        deathCounter++;
        if(deathCounter > 300)
        {
            Destroy(gameObject);
        }
    }
    public void setFlameMoveSpeed(int a)
    {
        moveSpeed = a;
    }
}
