using UnityEngine;
using System.Collections;

public class spawnObstacles : MonoBehaviour {

	public GameObject rocks;
	public float spawnInterval;
	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateObstacle", 1f, spawnInterval);
	}
	
	void CreateObstacle()
	{
		Instantiate(rocks);
	}
}
