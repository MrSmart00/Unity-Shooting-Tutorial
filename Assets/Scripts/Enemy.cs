using UnityEngine;
using System.Collections;

public class Enemy : Spaceship {

	// ヒットポイント
	public int hp = 1;

	// 弾を撃つかどうか
	public bool canShot;

	// スコアのポイント
	public int point = 100;

	IEnumerator Start ()
	{
		// ローカル座標のY軸のマイナス方向に移動する
		Move (transform.up * -1);
		
		// canShotがfalseの場合、ここでコルーチンを終了させる
		if (canShot == false) {
			yield break;
		}
		
		while (true) {
			
			// 子要素を全て取得する
			for (int i = 0; i < transform.childCount; i++) {
				
				Transform shotPosition = transform.GetChild (i);
				
				// ShotPositionの位置/角度で弾を撃つ
				Shot (shotPosition);
			}
			
			// shotDelay秒待つ
			yield return new WaitForSeconds (shotDelay);
		}
	}
	
	// 機体の移動
	protected override void Move (Vector2 direction)
	{
		rigidbody2D.velocity = direction * speed;
	}
	
	void OnTriggerEnter2D (Collider2D c)
	{
		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		
		// レイヤー名がBullet (Player)以外の時は何も行わない
		if (layerName != "Bullet (Player)") return;
		
		// PlayerBulletのTransformを取得
		Transform playerBulletTransform = c.transform.parent;
		
		// Bulletコンポーネントを取得
		Bullet bullet =  playerBulletTransform.GetComponent<Bullet>();
		
		// ヒットポイントを減らす
		hp = hp - bullet.power;
		
		// 弾の削除
		Destroy(c.gameObject);
		
		// ヒットポイントが0以下であれば
		if(hp <= 0 )
		{
			// スコアコンポーネントを取得してポイントを追加
			FindObjectOfType<Score>().AddPoint(point);
			
			// 爆発
			Explosion ();
			
			// エネミーの削除
			Destroy (gameObject);
			
		}else{
			
			GetComponent<Animator> ().SetTrigger("Damage");
			
		}
	}
}
