using UnityEngine;

//using UnityEngine.SocialPlatforms.GameCenter;

public class Manager : MonoBehaviour
{
	// Playerプレハブ
	public GameObject player;
	
	// タイトル
	public GameObject title;
	
	void Start ()
	{
		// Titleゲームオブジェクトを検索し取得する
		title = GameObject.Find ("Title");



//		GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
//		Social.localUser.Authenticate( success => {
//			if (success)
//				ReportAchievement();
//			else
//				
//				Debug.Log ("認証に失敗しました。");
//		});
	}

//	void ReportAchievement() {
//		
//		Social.ReportProgress( "Achievement01", 100, (result) => {
//			
//			Debug.Log ( result ? "Achievement を報告しました。" : "Achievement の報告に失敗しました。");
//		});
//		
//	}	

	void OnGUI ()
	{
		// ゲーム中ではなく、タッチまたはマウスクリック直後であればtrueを返す。
		if (IsPlaying () == false && Event.current.type == EventType.MouseDown) {
			GameStart ();
		}
	}
	
	void GameStart ()
	{
		// ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
		title.SetActive (false);
		Instantiate (player, player.transform.position, player.transform.rotation);
	}
	
	public void GameOver ()
	{
		FindObjectOfType<Score> ().Save ();
		// ゲームオーバー時に、タイトルを表示する
		title.SetActive (true);
	}
	
	public bool IsPlaying ()
	{
		// ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}
}