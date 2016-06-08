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

	bool gotoStart;
	bool gameStarted;

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
	}

	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			bool input = false;
			if (Input.touchCount > 0) {
				//Store the first touch detected.
				Touch myTouch = Input.touches [0];
				if (myTouch.phase == TouchPhase.Began) {
					input = true;
					gameOverText.enabled = true;
				}
			} else if (Input.GetButtonDown ("Fire1")) {
				input = true;
			}

			if (input) {
				gotoStart = true;
				gameStarted = true;
				startGameText.enabled = false;
				titleText.enabled = false;
				healthPointText.enabled = true;
			}
		}

		if (gotoStart) {
			mainCamera.transform.localPosition = Vector3.Lerp (mainCamera.transform.localPosition, cameraGamePosition, Time.deltaTime * 0.7f);
			Vector3 distance = mainCamera.transform.localPosition - cameraGamePosition;
			if (distance.magnitude < 0.5) {
				gotoStart = false;
				enemyManager.startGame ();
			}
		}
	}

	public void gameOver() {
		gameOverText.enabled = true;
	}
}
