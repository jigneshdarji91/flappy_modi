using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scoreKeeper : MonoBehaviour {
	
	static float score;
	static bool countScore;
	static int highscore;
	
	// Use this for initialization
	void Start () {
		
		highscore = PlayerPrefs.GetInt ("Highscore");
		score = 0;
		StartCounting ();
	}
	
	// Update is called once per frame
	void Update () {
		DisplayScore ();
	}

	public static void incrementScore()
	{
		score++;
	}

	void DisplayScore()
	{
		guiText.text = "S:" + (int)score + " H:" + (int) highscore;
	}
	
	public static void ResetScore() 
	{
		if ((int)score > highscore)
			PlayerPrefs.SetInt ("Highscore", (int)score);
		PlayerPrefs.SetInt ("PreviousScore", (int)score);
		score = 0;
		highscore = PlayerPrefs.GetInt ("Highscore");
		StopCounting ();
	}
	
	public static void StartCounting()
	{
		countScore = true;
	}
	
	public static void StopCounting()
	{
		countScore = false;
	}
}
