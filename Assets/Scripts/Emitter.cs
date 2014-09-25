using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour {
	
	public GameObject enemy;
	public GameObject managerObject;

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
			List<Vector2> positions = WaveData.instance.list[current];
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
			if (WaveData.instance.list.Count <= ++current) {
				current = 0;
			}
		}
	}
}
