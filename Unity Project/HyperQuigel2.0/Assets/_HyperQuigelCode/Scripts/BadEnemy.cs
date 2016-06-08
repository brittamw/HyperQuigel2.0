using UnityEngine;
using System.Collections;

public class BadEnemy : Enemy {
	
	public override void DoAction(bool rightAction) {
		if (rightAction) {
			nav.enabled = false;
			enemey.isKinematic = false;
			Vector3 force = new Vector3 (-5, 40, 50);
			enemey.AddForce (force, ForceMode.Impulse);
			alive = false;
			light.enabled = false;
			audioSource.clip = rightActionAudio;
		} else {
			nav.acceleration = 30;
			nav.speed = nav.speed + 30;
			alive = false;
			light.enabled = false;
			audioSource.clip = wrongActionAudio;
		}
		current = false;
		audioSource.Play ();
	}
}
