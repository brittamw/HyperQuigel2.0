using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

    Transform player;

	public bool alive;
	public Rigidbody enemey;
    public NavMeshAgent nav;
	public Light light;
	public AudioClip rightActionAudio;
	public AudioClip wrongActionAudio;
	public AudioSource audioSource;

	EnemyManager enemyManager;
	public PlayerHealth playerHealth;

    void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		enemyManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		playerHealth = GameObject.FindGameObjectWithTag ("PlayerHealth").gameObject.GetComponent<PlayerHealth> ();
		nav = GetComponent<NavMeshAgent>();
		light = GetComponentInChildren<Light> ();
		alive = true;
		enemey = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			nav.SetDestination (player.position);
		}
	}

	public abstract void DoAction (bool rightAction);

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("GameArea")) {
			enemyManager.newEnemyInGameArea (this);
		}
	}

	public void markAsEnabled() {
		light.enabled = true;
	}
}
