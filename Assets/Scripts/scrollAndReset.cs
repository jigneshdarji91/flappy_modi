using UnityEngine;
using System.Collections;

public class scrollAndReset : MonoBehaviour {

	public Vector2 velocity = new Vector2(-5, 0);
	float distanceFromCamera;
	float screenCrossPixelLeft;
	float screenCrossPixelRight;
	float leftX, rightX;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = velocity;
		distanceFromCamera = (transform.position - Camera.main.transform.position).z;

		leftX = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceFromCamera)).x;
		rightX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceFromCamera)).x;
		float extraSpace = gameObject.renderer.bounds.size.x - (rightX - leftX);
		screenCrossPixelLeft = leftX
			- gameObject.renderer.bounds.size.x / 2;
		screenCrossPixelRight = rightX
			+ gameObject.renderer.bounds.size.x / 2 
				+ extraSpace;

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
