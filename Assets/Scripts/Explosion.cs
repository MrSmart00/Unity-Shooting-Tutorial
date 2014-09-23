using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	[HideInInspector]
	public bool gameOver = false;

	void OnAnimationFinish ()
	{
		Destroy (gameObject);
		if (gameOver == true) {
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
