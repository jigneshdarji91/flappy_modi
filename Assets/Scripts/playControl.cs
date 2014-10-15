using UnityEngine;
using System.Collections;

public class playControl : MonoBehaviour {

	// Use this for initialization
	public Vector2 touchForce = new Vector2(0f, 2f);
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//Application.LoadLevel("StartMenu");
		}

		if(Input.GetMouseButtonDown(0)) 
		{
			//Debug.Log("playControl::Update Mouse input");
			rigidbody2D.velocity = Vector2.zero;
			transform.rigidbody2D.AddForce(touchForce, ForceMode2D.Impulse);
		}
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) 
		{
			//Debug.Log("playControl::Update Touch input");
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce(touchForce, ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("playControl::OnCollisionEnter2D GameOver()");
	}
}
