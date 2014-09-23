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
			if (waves == null || waves.Count == 0) {
				yield break;
			} else {
				List<Vector2> positions = waves[current];
				foreach (Vector2 pos in positions) {
					ObjectPool.instance.GetGameObject(enemy, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
				}
				bool vanished = false;
				while (!vanished) {
					foreach (Transform child in ObjectPool.instance.transform){
						GameObject cgo = child.gameObject;
						if (cgo.name.StartsWith("Enemy")) {
							vanished = !cgo.activeSelf;
							if (vanished == false) {
								break;
							}
						}
					}
					yield return new WaitForEndOfFrame ();
				}
				if (waves.Count <= ++current) {
					current = 0;
				}
			}
		}
	}
}
