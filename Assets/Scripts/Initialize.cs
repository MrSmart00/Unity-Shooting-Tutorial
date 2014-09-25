using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Linq;

public class Initialize : MonoBehaviour {

	private int max = 0;

	void Start () {
		ParseQuery<ParseObject> query = ParseObject.GetQuery("Wave");
		query.FindAsync().ContinueWith(t1 => {
			IEnumerable<ParseObject> r1 = t1.Result;
			max = r1.Count();
			foreach(ParseObject r in r1) {
				ParseRelation<ParseObject> enemies = r.Get<ParseRelation<ParseObject>>("enemy");
				enemies.Query.FindAsync().ContinueWith(t2 => {
					List<Vector2> positions = new List<Vector2> ();
					IEnumerable<ParseObject> r2 = t2.Result;
					foreach(ParseObject po in r2) {
						float x = po.Get<float>("x");
						float y = po.Get<float>("y");
						positions.Add (new Vector2 (x, y));
					}
					WaveData.instance.list.Add(positions);
				});
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (max > 0) {
			if(WaveData.instance.list.Count == max) {
				Application.LoadLevel("Stage");
			}
		}
	}
}
