using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// 弾の移動スピード
	[Range(0, 10)]
	public int speed = 10;

	// 攻撃力
	[Range(1, 10)]
	public int power = 1;
	
	private IEnumerator routine;

	void Start ()
	{
	}

	void OnEnable ()
	{
		rigidbody2D.velocity = transform.up.normalized * speed;
		routine = ManageLife (60);
		StartCoroutine (routine);
	}

	void OnDisable ()
	{
		StopCoroutine (routine);
	}

	private IEnumerator ManageLife (int max)
	{
		int start = Time.frameCount;
		int current = start;
		while (current < start + max) {
			current = Time.frameCount;
			yield return new WaitForEndOfFrame ();
		}

		if (current >= start + max) {
			ObjectPool.instance.ReleaseGameObject (gameObject);
			yield break;
		}
	}
}
