using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GoodEnemy goodEnemy;
	public BadEnemy badEnemy;

	ArrayList allEnemies;
	ArrayList enemiesInPlayArea;

	int currentEnemy;
	int timeBetweenEnemyKilling;
	public float timeBetweenEnemySpawning;

	bool gameRunning;

	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameRunning = false;
		allEnemies = new ArrayList ();
		enemiesInPlayArea = new ArrayList ();
		currentEnemy = -1;
		timeBetweenEnemyKilling = 0;
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			if (timeBetweenEnemySpawning == 0) {
				Invoke ("createEnemy", 0f);
				timeBetweenEnemySpawning = 300;
			}
			timeBetweenEnemySpawning--;
			
			if (timeBetweenEnemyKilling == 0) {
				bool leftClick = false;
				bool rightClick = false;
				if (Input.GetButtonDown ("Fire1")) {
					leftClick = true;
				} else if (Input.GetButtonDown ("Fire2")) {
					rightClick = true;
				}
				if (rightClick || leftClick) {
					if (currentEnemy < enemiesInPlayArea.Count) {
						Enemy enemy = (Enemy)enemiesInPlayArea [currentEnemy];
						if (enemy is GoodEnemy) {
							if (leftClick) {
								DoActionToEnemy (enemy, true);
							} else {
								DoActionToEnemy (enemy, false);
							}
						} else if (enemy is BadEnemy) {
							if (rightClick) {
								DoActionToEnemy (enemy, true);
							} else {
								DoActionToEnemy (enemy, false);
							}
						}
					}
				}		
			}

			if (timeBetweenEnemyKilling > 0) {
				timeBetweenEnemyKilling--;
			}
		}
	}

	void DoActionToEnemy(Enemy enemy, bool rightAction) {
		enemy.DoAction (rightAction);
		currentEnemy++;
		markCurrentEnemy ();

		timeBetweenEnemyKilling = 5;
		allEnemies.RemoveAt (0);
	}

	void createEnemy() {
		float randomForPos = Random.value-0.5f;
		Vector3 randomPos = new Vector3(randomForPos*40,2,20);

		float randomForType = Random.value*2f;
		Enemy newEnemyObject;
		if (Mathf.Floor(randomForType) == 0) {
			newEnemyObject = (Enemy) Instantiate (goodEnemy, randomPos, goodEnemy.transform.rotation);
		} else {
			newEnemyObject = (Enemy) Instantiate (badEnemy, randomPos, badEnemy.transform.rotation);
		}
		allEnemies.Add (newEnemyObject);
	}

	void markCurrentEnemy() {
		if (currentEnemy >= 0 && currentEnemy < enemiesInPlayArea.Count) {
			Enemy enemy = (Enemy)enemiesInPlayArea [currentEnemy];
			enemy.markAsEnabled ();
		}
	}

	public void newEnemyInGameArea(Enemy enemy) {
		enemiesInPlayArea.Add (enemy);
		if (currentEnemy == -1) {
			currentEnemy = 0;
		}
		markCurrentEnemy ();
	}

	public void startGame() {
		gameRunning = true;
	}

	public void endGame() {
		gameRunning = false;
		foreach(Enemy e in allEnemies) {
			e.DoAction (false);
		}

		gameManager.gameOver ();
	}
}
