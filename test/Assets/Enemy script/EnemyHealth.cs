using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int Health = 0;
    // Start is called before the first frame update
    void Start()
    {
        Health = 5;   
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void LowerHealth(int h)
    {
        Health = Health - h;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
