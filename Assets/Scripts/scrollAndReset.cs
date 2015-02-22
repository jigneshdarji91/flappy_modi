using UnityEngine;
using System.Collections;

public class scrollAndReset : MonoBehaviour {

	public Vector2 velocity = new Vector2(-5, 0);
	float distanceFromCamera;
	float screenCrossPixelLeft;
	float screenCrossPixelRight;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = velocity;
		distanceFromCamera = (transform.position - Camera.main.transform.position).z;
		screenCrossPixelLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceFromCamera)).x
			- gameObject.renderer.bounds.size.x / 2;
		screenCrossPixelRight = 1.3f * Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceFromCamera)).x
			+ gameObject.renderer.bounds.size.x / 2;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < screenCrossPixelLeft) 
		{
			transform.position = new Vector3 (screenCrossPixelRight, 
                         transform.position.y, 
                         transform.position.z);
		}
	}
}
