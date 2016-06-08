using UnityEngine;
using System.Collections;

public class GoodEnemy : Enemy {

	public override void DoAction(bool rightAction) {
		if (rightAction) {
			nav.enabled = false;
			alive = false;
			light.enabled = false;

			audioSource.clip = rightActionAudio;
			playerHealth.TakeDamage (-1);
			Destroy (this.gameObject, 1f);
		} else {
			nav.enabled = false;
			enemey.isKinematic = false;
			Vector3 force = new Vector3 (-5, 40, 50);
			enemey.AddForce (force, ForceMode.Impulse);
			alive = false;
			light.enabled = false;

			audioSource.clip = wrongActionAudio;
			playerHealth.TakeDamage (10);
		}
		current = false;
		audioSource.Play ();
	}
}
