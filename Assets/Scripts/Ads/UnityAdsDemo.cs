// UnityAdsDemo.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// This Unity Ads Demo script is a modified version of the example code provided within
//  the Integration Guide for Unity Asset Store Package found in the Unity Ads Knowledge Base:
//  http://unityads.unity3d.com/help/Documentation%20for%20Publishers/Integration-Guide-for-Unity-Asset-Store
//
// For a more detailed example, download and try out the following Unity Ads Demo project:
//  https://github.com/wcoastsands/unity-ads-demo
//
// Getting Started:
// From the Unity Ads Admin, make sure that the Monetization option 
//  for your game is enabled, and that at least one Ad Placement zone 
//  is set as default and enabled (http://unityads.unity3d.com/admin).
//
// Instructions:
// 1. Create a new Scene in Unity.
// 2. Create an new empty GameObject and rename it to Unity Ads Demo.
// 3. Attach this script to your GameObject called Unity Ads Demo.
// 4. With your Unity Ads Demo GameObject selected, 
//     enter in the Inspector panel: 
//      - your game ID (required), 
//      - your zone ID (optional), 
//      - and enable Test Mode (optional).
// 5. Build and Run this scene on your target device.
//
// Results:
// On application start, a button labeled "Waiting..." should appear 
//  in the upper left corner of your device's screen. Once it becomes 
//  enabled and says "Show Ad", press the button to show an ad.

using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsDemo : MonoBehaviour 
{
	public string gameID;
	public string zoneID;
	public bool disableTestMode;
	public bool showInfoLogs;
	public bool showDebugLogs;
	public bool showWarningLogs = true;
	public bool showErrorLogs = true;
	
	void Awake() 
	{
		// Make sure a game ID is provided.
		if (string.IsNullOrEmpty(gameID))
		{
			Debug.LogError("A valid game ID is required to initialize Unity Ads.");
		}
		// Check if the player is running on a supported platform: editor, iOS, Android.
		else if (Advertisement.isSupported) 
		{
			// NOTE: Assigning a value to allowPrecache doesn't actually do anything, 
			//        and calling the allowPrecache property always returns true.
			Advertisement.allowPrecache = true;
			
			// NOTE: Development Build must be set in Build Settings 
			//        to show additional debug levels from Unity Ads.
			Advertisement.debugLevel = Advertisement.DebugLevel.NONE;	
			if (showInfoLogs) Advertisement.debugLevel    |= Advertisement.DebugLevel.INFO;
			if (showDebugLogs) Advertisement.debugLevel   |= Advertisement.DebugLevel.DEBUG;
			if (showWarningLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.WARNING;
			if (showErrorLogs) Advertisement.debugLevel   |= Advertisement.DebugLevel.ERROR;
			
			// Enable test mode by default when Development Build is set in Build Settings.
			//  Disable it only when production mode testing is necessary. By checking to
			//  see if Development Build is set, we avoid accidentally submitting a 
			//  production build for review with test mode enabled.
			bool enableTestMode = Debug.isDebugBuild && !disableTestMode; 
			Debug.Log(string.Format("Initializing Unity Ads for game ID {0} with test mode {1}.",
			                        gameID, enableTestMode ? "enabled" : "disabled"));
			
			// Only call Initialize once throughout your game.
			Advertisement.Initialize(gameID,enableTestMode);

		} 
		else 
		{
			Debug.Log("Platform not supported with Unity Ads.");
		}
	}
	
	void OnGUI()
	{
		return;
		// Continue only if Unity Ads is initialized.
		if (!Advertisement.isInitialized) return;
		
		// Setting zone to null if the value for zoneID is an empty string,
		//  otherwise setting zone equal to the same value as zoneID.
		//
		// If zone is null when passed to the isReady() and Show() methods,
		//  both methods will use the default Ad Placement zone specified 
		//  in the Unity Ads Admin.
		//
		// To update which Ad Placement zone is used as the default, 
		//  visit http://unityads.unity3d.com/admin and select the game 
		//  profile you would like to make changes to. Then select the
		//  Magnetization Settings tab and the Show Advanced Settings button 
		//  to display the option for selecting a default Ad Placement zone.
		string zone = string.IsNullOrEmpty(zoneID) ? null : zoneID;
		
		// isReady will be evaluated each time OnGUI() is called,
		//  which happens once a frame.
		bool isReady = Advertisement.isReady(zone);
		
		// Enable the GUI button only when the Ad Placement zone is ready to be shown.
		GUI.enabled = isReady;
		
		// Set button text to "Show Ad" when the Ad Placement zone is ready to be shown,
		//  otherwise set button text to "Waiting..."
		if (GUI.Button(new Rect(10, 10, 150, 50), isReady ? "Show Ad" : "Waiting...")) 
		{
			// Tell us which zone is being used, and if ads are available for it.
			Debug.Log(string.Format("Ad Placement zone with ID of {0} is {1}.",
			                        string.IsNullOrEmpty(zone) ? "null" : zone,
			                        isReady ? "ready" : "not ready"));
			
			// Show the advertisement for the specified zone.
			if (isReady) ShowAd(zone);
		}
		
		// Re-enable the GUI for any additional UI elements.
		GUI.enabled = true;
	}
	
	public void ShowAd (string zone = null)
	{
		// If the value for zone is an empty, set it to null.
		//  When the zone value is null, the default zone will be used.
		if (string.IsNullOrEmpty(zone)) zone = null;
		
		ShowOptions options = new ShowOptions();
		
		// With the pause option set to true, the timeScale and AudioListener 
		//  volume for your game is set to 0 while the ad is shown.
		// NOTE: The current version of Unity Ads always pauses the app.
		options.pause = true;
		
		// Use the resultCallback action to call a method when an ad is closed.
		//  The method assigned should accept a parameter of type ShowResult.
		if (zone == "rewardedVideoZone" || zone == "incentivisedVideoZone")
		{
			options.resultCallback = HandleShowResultWithReward;
		}
		else
		{
			options.resultCallback = HandleShowResult;
		}
		
		// Show the ad with the specified zone and options.
		Advertisement.Show(zone,options);
	}

	// Use this callback method when handling rewarded video ads.
	private void HandleShowResultWithReward (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully completed. Rewarding the player...");
			//*** PLACE YOUR CODE HERE FOR REWARDING PLAYERS ***
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	// Use this callback method when handling non-rewarded ads.
	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully completed.");
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
