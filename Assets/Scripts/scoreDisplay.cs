using UnityEngine;
using System.Collections;

public class scoreDisplay : MonoBehaviour {
	
	TextMesh textMesh;
	public string scoreName;
	// Use this for initialization
	void Start () {

		guiText.text = "Score: " + PlayerPrefs.GetInt ("PreviousScore") + "\nHigh: " + PlayerPrefs.GetInt ("Highscore");
		Debug.Log (guiText.text);
		//GetComponent (TextMesh).text = PlayerPrefs.GetInt (scoreName);
		//textMesh = (TextMesh) GetComponent(typeof(TextMesh));
		//textMesh.text = "" + PlayerPrefs.GetInt (scoreName);
	}
	
	// Update is called once per frame
	void Update () {
		//guiText = PlayerPrefs.GetInt (scoreName);
	}
}
