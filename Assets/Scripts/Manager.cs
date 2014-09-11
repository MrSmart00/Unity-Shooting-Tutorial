using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	// Playerプレハブ
	public GameObject player;
	
	// タイトル
	public Canvas title;

	void Update ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			
			// タッチ情報を取得する
			Touch touch = Input.GetTouch (i);
			
			// ゲーム中ではなく、タッチ直後であればtrueを返す。
			if (IsPlaying () == false && touch.phase == TouchPhase.Began) {
				GameStart ();
			}
		}
		
		// ゲーム中ではなく、マウスクリックされたらtrueを返す。
		if (IsPlaying () == false && Input.GetMouseButtonDown (0)) {
			GameStart ();
		}

	}
	
	void GameStart ()
	{
		// ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
		title.enabled = false;
		Instantiate (player, player.transform.position, player.transform.rotation);
	}
	
	public void GameOver ()
	{
		// ハイスコアの保存
		FindObjectOfType<Score>().Save();

		// ゲームオーバー時に、タイトルを表示する
		title.enabled = true;
	}

	public bool IsPlaying ()
	{
		// ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.enabled == false;
	}
}
