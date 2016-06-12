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
	bool firstStart;

	public GameManager gameManager;
	public PlayerHealth playerHealth;

	float currentNavSpeed;
	int spawnTime;

	// Use this for initialization
	void Start () {
		gameRunning = false;
		firstStart = true;
		allEnemies = new ArrayList ();
		enemiesInPlayArea = new ArrayList ();
		currentEnemy = -1;
		timeBetweenEnemyKilling = 0;
		currentNavSpeed = 3f;
		spawnTime = 300;
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			if (firstStart) {
				InvokeRepeating ("makeGameHarder", 5f, 5f);
				firstStart = false;
			}
			if (timeBetweenEnemySpawning == 0) {
				Invoke ("createEnemy", 0f);
				timeBetweenEnemySpawning = spawnTime;
			}
			timeBetweenEnemySpawning--;
		}
	}

	public void beat() {
		if (gameRunning) {
			if (currentEnemy < enemiesInPlayArea.Count) {
				Enemy enemy = (Enemy)enemiesInPlayArea [currentEnemy];
				if (enemy is GoodEnemy) {
					DoActionToEnemy (enemy, false);
				} else if (enemy is BadEnemy) {
					DoActionToEnemy (enemy, true);				
				}
			}
		}
	}

	public void brush() {
		if (gameRunning) {
			if (currentEnemy < enemiesInPlayArea.Count) {
				Enemy enemy = (Enemy)enemiesInPlayArea [currentEnemy];
				if (enemy is GoodEnemy) {
					DoActionToEnemy (enemy, true);
				} else if (enemy is BadEnemy) {
					DoActionToEnemy (enemy, false);				
				}
			}
		}
	}

	void DoActionToEnemy(Enemy enemy, bool rightAction) {
		enemy.DoAction (rightAction);
		currentEnemy++;
		markCurrentEnemy ();

		timeBetweenEnemyKilling = 5;
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

	public void enemyTooNear(Enemy enemy) {
		if (enemy.isCurrent()) {
			currentEnemy++;
			markCurrentEnemy ();
		}
		if (enemy is BadEnemy) {
			playerHealth.TakeDamage (10);
		}
		Destroy (enemy.gameObject);
	}

	public float getCurrentNavSpeed() {
		return currentNavSpeed;
	}

	public void makeGameHarder() {
		spawnTime = spawnTime - 30;
		if (spawnTime < 30) {
			spawnTime = 30;
		}

		currentNavSpeed = currentNavSpeed + 0.5f;
		Debug.Log ("Game is now harder: speed=" + currentNavSpeed + " spawntime: " + timeBetweenEnemySpawning);
	}

	public void startGame() {
		gameRunning = true;
	}

	public void endGame() {
		gameRunning = false;
		foreach(Enemy e in allEnemies) {
			if (e != null && e.gameObject != null) {
				e.audioSource.enabled = false;
				e.DoAction (false);
			}
		}

		gameManager.gameOver ();
	}
}
