﻿using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GoodEnemy goodEnemy;
	public BadEnemy badEnemy;

	ArrayList enemyList;

	int currentEnemy;
	int timeBetweenEnemyKilling;
	public float timeBetweenEnemySpawning;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createEnemy", 1f, timeBetweenEnemySpawning);
		enemyList = new ArrayList ();
		currentEnemy = -1;
		timeBetweenEnemyKilling = 0;
	}

	// Update is called once per frame
	void Update () {
		if (timeBetweenEnemyKilling == 0) {
			bool leftClick = false;
			bool rightClick = false;
			if (Input.GetButtonDown ("Fire1")) {
				leftClick = true;
			} else if (Input.GetButtonDown("Fire2")) {
				rightClick = true;
			}
			if (rightClick || leftClick) {
				if (currentEnemy < enemyList.Count) {
					Enemy enemy = (Enemy) enemyList [currentEnemy];
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
	}

	void markCurrentEnemy() {
		if (currentEnemy >= 0 && currentEnemy < enemyList.Count) {
			Enemy enemy = (Enemy)enemyList [currentEnemy];
			enemy.markAsEnabled ();
		}
	}

	public void newEnemyInGameArea(Enemy enemy) {
		enemyList.Add (enemy);
		if (currentEnemy == -1) {
			currentEnemy = 0;
		}
		markCurrentEnemy ();
	}
}
