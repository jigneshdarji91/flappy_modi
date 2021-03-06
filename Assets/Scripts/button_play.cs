﻿using UnityEngine;
using System.Collections;

public class button_play : MonoBehaviour {
	
	int buttonHeight, buttonWidth;
	public Texture buttonSprite;
	public string sceneName;
	public Vector2 relativeButtonSize;
	public Vector2 relativeButtonLocation;
	public GUIStyle guiStyle;
	public bool isVibrate;

	void Start()
	{
		buttonWidth = (int) (Screen.width * relativeButtonSize.x);
		buttonHeight = (int) buttonSprite.height * buttonWidth / buttonSprite.width;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//Application.LoadLevel("StartMenu");
		}
	}
	
	void OnGUI()
	{
		
		// Determine the button's place on screen
		Rect buttonRect = new Rect(
			(Screen.width * relativeButtonLocation.x) - (buttonWidth / 2),
			(Screen.height * relativeButtonLocation.y) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight);
		
		// Draw a button to start the game
		if(GUI.Button(buttonRect, buttonSprite, guiStyle))
		{
			if(isVibrate)
			{
				Handheld.Vibrate();
			}
			if(sceneName.Equals("Quit"))
			{
				Debug.Log("Quit Button Pressed");
				Application.Quit();
			}
			else if(sceneName.Equals("0"))
			{
				Debug.Log("Clicked on title image");
			}
			else if(sceneName.Equals("Rate"))
			{
				Debug.Log("Rate Button Pressed");
				Application.OpenURL("market://details?id=com.CasualGameAlliance.FlappyModi");
			}
			else if(sceneName.Equals("Share"))
			{
				Debug.Log("Share Button Pressed");
			}
			else
			{
				Debug.Log("Play Button Pressed");
				Application.LoadLevel(sceneName);
			}
		}
	}
}
