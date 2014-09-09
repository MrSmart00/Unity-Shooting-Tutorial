using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	// Playerプレハブ
	public GameObject player;
	
	// タイトル
	public Canvas title;

	void Start ()
	{
		foreach(object n in Coroutine ()) {
			Debug.Log(n);
		}
	}

	void Update ()
	{
		// ゲーム中ではなく、Xキーが押されたらtrueを返す。
		if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X)) {
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

	private IEnumerable Coroutine()
	{
		Debug.Log ("**********");
		yield return 1;
		yield return 10;
		yield return 100;
		yield return "ネコミミモード";
		yield break;
	}
}
