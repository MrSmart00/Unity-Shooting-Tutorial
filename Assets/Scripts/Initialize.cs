using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Initialize : MonoBehaviour {
	
	public Canvas canvas;
	public GameObject emitter;

	// Use this for initialization
	void Start () {
//		string storagePath = Application.persistentDataPath + "/storage";
//		Debug.Log ("PATH : " + storagePath);
		StartCoroutine (ConnectHome("http://localhost/"));
		StartCoroutine (ConnectWave("http://localhost/wave.php"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private IEnumerator ConnectHome(string url) {
		WWW www = new WWW (url);
		yield return www;
		IList json = (IList)Json.Deserialize(www.text);
		foreach (IDictionary item in json) {
			Text target = null;
			foreach (Transform child in canvas.transform){
				if(child.name == "Main Title"){
					target = child.gameObject.GetComponent<Text>();
					target.text = (string)item["title"];
				} else if (child.name == "Sub Title") {
					target = child.gameObject.GetComponent<Text>();
					target.text = (string)item["subtitle"];
				}
			}
		}
		if (www.error != null) {
			Debug.Log("Failure : " + www.error);
		}
		yield break;
	}

	private IEnumerator ConnectWave(string url) {
		WWW www = new WWW (url);
		yield return www;
		IList json = (IList)Json.Deserialize(www.text);
		List<List<Vector2>> list = new List<List<Vector2>> ();
		foreach (IList l in json) {
			List<Vector2> positions = new List<Vector2>();
			foreach (IDictionary item in l) {
				float x = float.Parse((string)item["x"]);
				float y = float.Parse((string)item["y"]);
				positions.Add(new Vector2 (x, y));
			}
			list.Add(positions);
		}
		if (www.error == null) {
			Emitter e = emitter.GetComponent<Emitter>();
			e.waves = list;
		} else {
			Debug.Log("Failure : " + www.error);
		}
		yield break;
	}

}
