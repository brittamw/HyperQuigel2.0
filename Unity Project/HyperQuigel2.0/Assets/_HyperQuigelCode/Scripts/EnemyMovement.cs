using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    Transform player;

	bool alive;
	bool dying;
	bool currentEnemy;

	Rigidbody enemey;
    NavMeshAgent nav;
	// Use this for initialization

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
		alive = true;
		dying = false;
		currentEnemy = true;
		enemey = GetComponent<Rigidbody> ();
    }


    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentEnemy) {
			if (Input.GetButtonDown ("Fire1")) {
				dying = true;
			}

			if (dying) {
				Die ();
			}

			if (alive) {
				nav.SetDestination (player.position);
			}
		}		 
	}

	void Die() {
		nav.enabled = false;
		Vector3 force = new Vector3 (-5, 20, 15);
		enemey.AddForce (force, ForceMode.Impulse);
		dying = false;
		alive = false;
		currentEnemy = false;
	}
}
