using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour {

	float timeBetweenActions = 0.5f;
	float timeBetweenThumbs = 2f;
	float currentTimeBetweenActions = 0;
	float currentTimeBetweenThumbs = 0;
	public EnemyManager enemyManager;
	public GameManager gameManager;

	bool gameOnceStarted = false;


	protected virtual void Update() {
		if (currentTimeBetweenActions >= 0) {
			currentTimeBetweenActions = currentTimeBetweenActions - 1 * Time.deltaTime;
		} 
		if (currentTimeBetweenThumbs >= 0) {
			currentTimeBetweenThumbs = currentTimeBetweenThumbs - 1 * Time.deltaTime;
		}
	}

	public void beat () {
		if (currentTimeBetweenActions < 0) {
			Debug.Log ("beat");
			enemyManager.beat ();
			currentTimeBetweenActions = timeBetweenActions;
		}
	}

	public void brush() {
		if (currentTimeBetweenActions < 0) {
			Debug.Log ("brush");
			enemyManager.brush ();
			currentTimeBetweenActions = timeBetweenActions;
		}
	}

	public void startGame () {
		if (!gameOnceStarted) {
			if (currentTimeBetweenThumbs < 0) {
				Debug.Log ("thumbUp");
				gameManager.goToStart ();
				currentTimeBetweenThumbs = timeBetweenActions;
				gameOnceStarted = true;
			}
		}
	}
}
