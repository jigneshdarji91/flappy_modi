using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		const string projectId = "c4e2400d-5f36-4fa9-b893-4a2aa69eafdf";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}