using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class LeapMotionController : InputController {

	LeapServiceProvider leapProvider;

	protected void Awake () {
		leapProvider = FindObjectOfType<LeapServiceProvider>();
	}

	protected override void Update() {
		base.Update ();
		Frame frame = leapProvider.CurrentFrame;
		foreach (Hand hand in frame.Hands) {
			bool handFast = hand.PalmVelocity.y < -0.8;
			bool handOpen = hand.GrabAngle < 2.3;
			bool thumbExtended = hand.Fingers [0].IsExtended;

			if (handFast && !handOpen) {
				beat ();
			} else if (handFast && handOpen) {
				brush ();
			}

			if (thumbExtended && !handOpen) {
				startGame ();
			}


			//Debug.Log ("velo: " + hand.PalmVelocity.y);
			//Debug.Log ("closed: " + handOpen);

		}
	}
}
