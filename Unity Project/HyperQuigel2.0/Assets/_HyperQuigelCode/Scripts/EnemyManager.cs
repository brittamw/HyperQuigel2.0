using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GoodEnemy goodEnemy;
	public BadEnemy badEnemy;

	ArrayList enemyList;

	int currentEnemy;
	int timeBetweenEnemyKilling;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createEnemy", 0f, 2f);
		enemyList = new ArrayList ();
		currentEnemy = -1;
		timeBetweenEnemyKilling = 0;
	}

	// Update is called once per frame
	void Update () {
		if (timeBetweenEnemyKilling == 0) {
			bool killGoodEnemy = false;
			bool killBadEnemy = false;
			if (Input.GetButtonDown ("Fire1")) {
				killGoodEnemy = true;
			} else if (Input.GetButtonDown("Fire2")) {
				killBadEnemy = true;
			}
			if (killBadEnemy || killGoodEnemy) {
				if (currentEnemy < enemyList.Count) {
					Enemy enemy = (Enemy) enemyList [currentEnemy];
					if (enemy is GoodEnemy && killGoodEnemy) {
						KillEnemy (enemy);
					} else if (enemy is BadEnemy && killBadEnemy) {
						KillEnemy (enemy);
					}
				}
			}		
		}

		if (timeBetweenEnemyKilling > 0) {
			timeBetweenEnemyKilling--;
		}
	}

	void KillEnemy(Enemy enemy) {
		enemy.Die ();
		currentEnemy++;
		timeBetweenEnemyKilling = 5;
	}

	void createEnemy() {
		float randomForPos = Random.value-0.5f;
		Vector3 randomPos = new Vector3(randomForPos*50,0,0);

		float randomForType = Random.value*2f;
		Enemy newEnemyObject;
		if (Mathf.Floor(randomForType) == 0) {
			newEnemyObject = (Enemy) Instantiate (goodEnemy, randomPos, goodEnemy.transform.rotation);
		} else {
			newEnemyObject = (Enemy) Instantiate (badEnemy, randomPos, badEnemy.transform.rotation);
		}

		enemyList.Add (newEnemyObject);
		// nur beim ersten Enemy
		if (currentEnemy == -1) {
			currentEnemy = 0;
		}
	}
}
