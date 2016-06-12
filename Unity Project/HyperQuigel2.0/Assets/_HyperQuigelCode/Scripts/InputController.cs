using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour {

	float timeBetweenActions = 0.5f;
	float currentTime = 0;
	public EnemyManager enemyManager;
	public GameManager gameManager;


	protected virtual void Update() {
		if (currentTime >= 0) {
			currentTime = currentTime - 1 * Time.deltaTime;
		} 
	}

	public void beat () {
		if (currentTime < 0) {
			Debug.Log ("beat");
			enemyManager.beat ();
			currentTime = timeBetweenActions;
		}
	}

	public void brush() {
		if (currentTime < 0) {
			Debug.Log ("brush");
			enemyManager.brush ();
			currentTime = timeBetweenActions;
		}
	}

	public void startGame () {
		if (currentTime < 0) {
			Debug.Log ("thumbUp");
			gameManager.goToStart();
			currentTime = timeBetweenActions;
		}
	}
}
