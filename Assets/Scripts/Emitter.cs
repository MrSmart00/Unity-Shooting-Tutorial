using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour {
	
	public GameObject enemy;
	public GameObject managerObject;
	[HideInInspector]
	public List<List<Vector2>> waves;
	
	void Start ()
	{
		StartCoroutine (createWave ());
	}
	
	private IEnumerator createWave ()
	{
		int current = 0;
		Manager manager = managerObject.GetComponent<Manager> ();
		while (true) {
			while(manager.IsPlaying() == false) {
				yield return new WaitForEndOfFrame ();
			}
			List<Vector2> positions = waves[current];
			List<GameObject> enemies = new List<GameObject>();
			foreach (Vector2 pos in positions) {
				GameObject e = (GameObject) Instantiate(enemy, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
				e.transform.parent = transform;
				enemies.Add(e);
			}
			while (transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}
			if (waves.Count <= ++current) {
				current = 0;
			}
		}
	}
}
