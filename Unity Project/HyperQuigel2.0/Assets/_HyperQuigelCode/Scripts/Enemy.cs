using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Transform player;

	bool alive;

	Rigidbody enemey;
    NavMeshAgent nav;

	Light light;

	EnemyManager enemyManager;

    void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		enemyManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		nav = GetComponent<NavMeshAgent>();
		light = GetComponentInChildren<Light> ();
		alive = true;
		enemey = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			nav.SetDestination (player.position);
		}
	}

	public void Die() {
		nav.enabled = false;
		enemey.isKinematic = false;
		Vector3 force = new Vector3 (-5, 40, 50);
		enemey.AddForce (force, ForceMode.Impulse);
		alive = false;
		light.enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("GameArea")) {
			enemyManager.newEnemyInGameArea (this);
		}
	}

	public void markAsEnabled() {
		light.enabled = true;
	}
}
