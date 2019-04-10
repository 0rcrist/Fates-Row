using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    { 
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.transform.position = GameObject.Find("Spawner").transform.position;
        }
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = false ;
    }

   
}
