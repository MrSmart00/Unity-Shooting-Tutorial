using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// 移動スピード
	public float speed = 5;
	
	void Update ()
	{
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");
		
		// 上・下
		float y = Input.GetAxisRaw ("Vertical");
		
		// 移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;
		
		// 移動する向きとスピードを代入する
		rigidbody2D.velocity = direction * speed;
	}
}
