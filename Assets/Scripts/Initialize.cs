using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Initialize : MonoBehaviour {
	
	public Canvas canvas;
	public GameObject emitter;

	public static string domain = "http://localhost/";

	void Start () {
		StartCoroutine (ConnectHome(domain));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private IEnumerator ConnectHome(string url) {
		WWW www = new WWW (url);
		yield return www;
		if (www.error == null) {
			IList json = (IList)Json.Deserialize(www.text);
			foreach (IDictionary item in json) {
				Text target = null;
				foreach (Transform child in canvas.transform){
					target = child.gameObject.GetComponent<Text>();
					if(child.name == "Main Title"){
						target.text = (string)item["title"];
					} else if (child.name == "Sub Title") {
						target.text = (string)item["subtitle"];
					}
				}
			}
			StartCoroutine (ConnectWave(domain + "wave.php"));
		} else {
			Text target = null;
			foreach (Transform child in canvas.transform){
				target = child.gameObject.GetComponent<Text>();
				if(child.name == "Main Title"){
					target.text = "ERROR!!";
				} else if (child.name == "Sub Title") {
					target.text = www.error.ToString();
				}
				target.color = Color.red;
			}
		}
		yield break;
	}

	private IEnumerator ConnectWave(string url) {
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		if (www.error == null) {
			IList json = (IList)Json.Deserialize (www.text);
			List<List<Vector2>> list = new List<List<Vector2>> ();
			foreach (IList l in json) {
				List<Vector2> positions = new List<Vector2> ();
				foreach (IDictionary item in l) {
					float x = float.Parse ((string)item ["x"]);
					float y = float.Parse ((string)item ["y"]);
					positions.Add (new Vector2 (x, y));
				}
				list.Add (positions);
			}
			Emitter e = emitter.GetComponent<Emitter> ();
			e.waves = list;
		} else {
			Debug.Log(www.error.ToString());
		}
		yield break;
	}

}
