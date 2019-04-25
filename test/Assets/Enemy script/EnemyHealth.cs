using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int Health; 
    Rigidbody2D myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void LowerHealth(int h)
    {
        Health = Health - h;
        DealWithHitBack();
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void DealWithHitBack()
    {
       string enemyname = GetComponent<Animator>().name;
        Debug.Log(enemyname);
        switch(enemyname)
        {
            case "Enemy Possum": BroadcastMessage("PossumFreeze", 0);
                break;
            case "Enemy Frog": BroadcastMessage("FrogFreeze", 0);
                break;

        }
    }
}
