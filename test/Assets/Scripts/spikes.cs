using UnityEngine;
using System.Collections;

public class spikes : MonoBehaviour
{
    public int damage;

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D Colider)
    {

        if (Colider.gameObject.tag == "Player")
        {
            Debug.Log("spike collision detected");
            Debug.Log("applied damage to player");
        }
    }
}