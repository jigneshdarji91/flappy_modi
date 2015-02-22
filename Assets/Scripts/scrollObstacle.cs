using UnityEngine;
using System.Collections;

public class scrollObstacle : MonoBehaviour {

	public Vector2 velocity = new Vector2(-5, 0);
	public float range = 4;
	bool crossedModi = false;
	float distanceFromCamera;
	float screenCrossPixelLeft;
	float screenCrossPixelRight;
	float modiPoint;

	// Use this for initialization
	void Start()
	{
		rigidbody2D.velocity = velocity;

		distanceFromCamera = (transform.position - Camera.main.transform.position).z;
		screenCrossPixelLeft = 1.3f * Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceFromCamera)).x;
		screenCrossPixelRight = 1.3f * Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceFromCamera)).x;
		modiPoint = Camera.main.ViewportToWorldPoint (new Vector3 (0.25f, 0, distanceFromCamera)).x;

		transform.position = new Vector3(screenCrossPixelRight, 
		                                 transform.position.y - range * Random.value, 
		                                 transform.position.z);
		Debug.Log("scrollObstacle::Update() Right: " + screenCrossPixelRight + " Left: " + screenCrossPixelLeft);
	} 
	void Update ()
	{
		InvokeRepeating("CheckLocation", 1f, 1f);
	}
		
	void CheckLocation()
	{
		if (transform.position.x < modiPoint) 
		{
			if (!crossedModi) 
			{
				crossedModi = true;
				scoreKeeper.incrementScore ();
			}
			if (transform.position.x < screenCrossPixelLeft) 
			{
				//Destroying this prefab obstacle
				Destroy (gameObject);
			} 
		}
	}
}
