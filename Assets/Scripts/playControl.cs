using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class playControl : MonoBehaviour {

	// Use this for initialization
	public Vector2 touchForce = new Vector2(0f, 2f);
	public AudioClip touchSound;
	public AudioClip deathSound;

	public int fontSize;

	//Global variable for Ads
	public bool showAds;
	public bool enableTestMode;
	public string zoneId;
	public string gameId;
	public int showAdsAfter;
	static int gamesPlayed;

	static bool gameStarted = false;


	void Start () {
		gamesPlayed = PlayerPrefs.GetInt ("GamesPlayed");
		StartCoroutine (InitializeAds());
		float distanceFromCamera = (transform.position - Camera.main.transform.position).z;
		float spawnPoint = Camera.main.ViewportToWorldPoint (new Vector3 (0.25f, 0, distanceFromCamera)).x;
		transform.position = new Vector3(spawnPoint, 
		                                 0f, 
		                                 transform.position.z);

		audio.clip = touchSound;

		pauseGame (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (! renderer.isVisible)
		{
			Debug.Log("playControl::Update() Out of screen");
			//Die ();
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//Application.LoadLevel("StartMenu");
		}

		if(Input.GetMouseButtonDown(0) ||
		   (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) 
		{
			pauseGame(false);

			//Debug.Log("playControl::Update Touch input");
			audio.Play();
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce(touchForce, ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("playControl::OnCollisionEnter2D GameOver()");
		Die ();
	}

	void Die()
	{
		Debug.Log("playControl::Die()");
		gamesPlayed++;
		PlayerPrefs.SetInt ("GamesPlayed", gamesPlayed);

		//audio.clip = deathSound;
		//audio.PlayScheduled (audio.clip.length);
		Handheld.Vibrate();
		PlayDeathSound ();
		ShowAd ();


		scoreKeeper.ResetScore ();
		Application.LoadLevel("StartMenu");

	}
	IEnumerator PlayDeathSound() {
		Debug.Log("playControl::PlayDeathSound()");
		audio.clip = deathSound;
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
	}

	void pauseGame(bool gamePaused)
	{
		if (gamePaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1.0f;
		gameStarted = !gamePaused;
	}

	void OnGUI()
	{
		if(!gameStarted)
		{
			//Set the GUIStyle style to be label
			GUIStyle style = GUI.skin.GetStyle ("label");
			
			//Set the style alignment
			style.alignment = TextAnchor.LowerCenter;
			style.fontSize = fontSize;
	
			//Create a label and display with the current settings
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "Tap To Start!");
		}
	}

	IEnumerator InitializeAds()
	{
		Debug.Log("InitializeAds BEGIN");
		if(showAds)
		{
			if (Advertisement.isSupported) {
				Advertisement.allowPrecache = true;
				Advertisement.Initialize (gameId, enableTestMode);
			} else {
				Debug.Log("Platform not supported");
			}
		}
		yield return new WaitForSeconds(0);
	}

	void ShowAd()
	{
		Debug.Log ("ShowAd AdInitialized: " + Advertisement.isInitialized
						+ " AdReady: " + Advertisement.isReady(zoneId)
						+ " gamesPlayed: " + gamesPlayed);
		//Show an ad if its loaded already and 5 games have been played
		if (Advertisement.isInitialized
		    && Advertisement.isReady ()
		    && gamesPlayed % showAdsAfter == 0) 
		{
			Advertisement.Show ();
			Debug.Log ("playControl::Die() Advertisement Shown");
		}
		else
			Debug.LogError("playControl::Die() Advertisement not ready");
	}
}
