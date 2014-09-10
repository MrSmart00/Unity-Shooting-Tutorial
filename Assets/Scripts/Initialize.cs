using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Connect("http://localhost/"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator Connect(string url) {
		WWW www = new WWW (url);
		yield return www;
		if (www.error == null) {
			Debug.Log("Success : " + www.text);
		} else {
			Debug.Log("Failure : " + www.error);
		}
	}

}
