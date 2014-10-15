using UnityEngine;
using System.Collections;

public class scrollObstacle : MonoBehaviour {

	public Vector2 velocity = new Vector2(-4, 0);
	public float range = 4;
	bool crossedModi = false;
	// Use this for initialization
	void Start()
	{
		rigidbody2D.velocity = velocity;
		transform.position = new Vector3(transform.position.x, 
		                                 transform.position.y - range * Random.value, 
		                                 transform.position.z);
	}
	void Update ()
	{
		if (transform.position.x < Screen.width / 2 && !crossedModi) 
		{
			Debug.Log ("scrollObstacle::Update() X: " + transform.position.x);
			crossedModi = true;
			scoreKeeper.incrementScore();
		}
	}
}
