using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void OnAnimationFinish ()
	{
		Destroy (gameObject);
	}
}
