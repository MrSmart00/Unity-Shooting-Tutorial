using UnityEngine;
using System.Collections;

public class Player : Spaceship
{
	// Backgroundコンポーネント
	private Background background;

	private Vector2 offset;

	[Range(0, 100)]
	[Tooltip("移動値の抵抗力設定（大きくなる程移動が遅くなる）")]
	public float transferResistance = 20;

	IEnumerator Start ()
	{
		// Backgroundコンポーネントを取得。3つのうちどれか1つを取得する
		background = FindObjectOfType<Background> ();
		
		while (true) {
			
			// 弾をプレイヤーと同じ位置/角度で作成
			Shot (transform);
			
			// ショット音を鳴らす
			audio.Play ();
			
			// shotDelay秒待つ
			yield return new WaitForSeconds (shotDelay);
		}
	}

	void Update ()
	{
		if (Input.touches.Length > 0) {
			Touch touch = Input.touches [0];
			switch (touch.phase) {
			case TouchPhase.Began:
				offset = touch.position;
				break;
			case TouchPhase.Moved:
				Vector2 move = touch.position;
				Vector2 diff = offset - move;
				Move(diff / -transferResistance);
				offset = move;
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				offset = Vector2.zero;
				break;
			}
		}
	}
	
	// 機体の移動
	protected override void Move (Vector2 direction)
	{
		// 背景のスケール
		Vector2 scale = background.transform.localScale;
		
		// 背景のスケールから取得
		Vector2 min = scale * -0.5f;
		
		// 背景のスケールから取得
		Vector2 max = scale * 0.5f;
		
		// プレイヤーの座標を取得
		Vector3 pos = transform.position;
		
		// 移動量を加える
		pos += (Vector3)direction * speed * Time.deltaTime;
		
		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}
	
	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D (Collider2D c)
	{
		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		
		// レイヤー名がBullet (Enemy)の時は弾を削除
		if (layerName == "Bullet (Enemy)") {
			// 弾の削除
			ObjectPool.instance.ReleaseGameObject(c.gameObject);
		}
		
		// レイヤー名がBullet (Enemy)またはEnemyの場合は爆発
		if (layerName == "Bullet (Enemy)" || layerName == "Enemy") {
			// Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			FindObjectOfType<Manager> ().GameOver ();
			
			// 爆発する
			Explosion (true);

			// プレイヤーを削除
			Destroy (gameObject);
		}
	}
}