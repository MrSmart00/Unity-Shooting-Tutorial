using UnityEngine;
using System.Collections.Generic;

public class WaveData {

	private static WaveData _instance;
	
	private WaveData () {
		list = new List<List<Vector2>> ();
	}
	
	public static WaveData instance {
		get
		{
			if( _instance == null ) _instance = new WaveData();
			return _instance;
		}
	}
	
	public List<List<Vector2>> list;
}
