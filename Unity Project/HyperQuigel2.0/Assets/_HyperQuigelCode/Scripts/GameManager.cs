using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	Vector3 cameraMenuPosition;
	Vector3 cameraGamePosition;
	public Camera mainCamera;
	public EnemyManager enemyManager;
	public UnityEngine.UI.Text titleText;
	public UnityEngine.UI.Text startGameText;
	public UnityEngine.UI.Text gameOverText;
	public UnityEngine.UI.Text healthPointText;
	public UnityEngine.UI.Text timeText;
	public UnityEngine.UI.Text gameOverTimeText;

	bool gotoStart;
	bool gameStarted;
	bool timerStarted;

	float timer;

	// Use this for initialization
	void Start () {
		cameraMenuPosition = new Vector3 (-0.54f, 10.8f, 20.48f);
		cameraGamePosition = new Vector3 (0, 0, 0);
		gotoStart = false;
		gameStarted = false;
		gameOverText.enabled = false;
		startGameText.enabled = true;
		titleText.enabled = true;
		healthPointText.enabled = false;
		timeText.enabled = false;
		timerStarted = false;
		timer = 0;
		gameOverTimeText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (gotoStart) {
			mainCamera.transform.localPosition = Vector3.Lerp (mainCamera.transform.localPosition, cameraGamePosition, Time.deltaTime * 0.7f);
			Vector3 distance = mainCamera.transform.localPosition - cameraGamePosition;
			if (distance.magnitude < 0.5) {
				gotoStart = false;
				enemyManager.startGame ();
				startTimer ();
			}
		}
		if (timerStarted == true) {
			timer += Time.deltaTime;
			string minutes = (string) Mathf.Floor(timer / 60).ToString("00");
			string seconds = (string) Mathf.Floor(timer % 60).ToString("00");
			timeText.text = minutes + ":" + seconds;
		}     
	}

	public void goToStart() {
		gotoStart = true;
		gameStarted = true;
		startGameText.enabled = false;
		titleText.enabled = false;
		healthPointText.enabled = true;
	}

	void startTimer() {
		timerStarted = true;
		timeText.enabled = true;
	}

	public void gameOver() {
		gameOverText.enabled = true;
		timerStarted = false;
		gameOverTimeText.enabled = true;
		gameOverTimeText.text = timeText.text;
		healthPointText.enabled = false;
		timeText.enabled = false;
	}
}
