using UnityEngine;
using System.Collections;

public class CollisionSceneChange : MonoBehaviour
{
	public string level;

	// Use this for initialization
	void OnTriggerEnter2D (Collision2D Colider)
	{
		if(Colider.gameObject.tag == "Player")
				Application.LoadLevel(level);
	}
}