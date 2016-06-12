using UnityEngine;
using System.Collections;

public class MouseController : InputController {
	protected override void Update() {
		base.Update ();

		if (Input.GetButtonDown("Fire1")) {
			startGame ();
			brush ();
		} else if (Input.GetButtonDown("Fire2")) {
			beat();
		}
	}
}
