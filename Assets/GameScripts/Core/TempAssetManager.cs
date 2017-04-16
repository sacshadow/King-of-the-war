using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

[CreateAssetMenuAttribute(fileName = "TempAssetManager", menuName = "TempAssetManager", order = 10)]
public class TempAssetManager : ScriptableObject {
	public static TempAssetManager Instance {
		get {
			if(singleton == null)
				singleton = Resources.Load<TempAssetManager>("TempAssetManager");
			return singleton;
		}
	}
	
	private static TempAssetManager singleton;
	
	public Material[] forceMat;
	
	public Solider solider;
	
	
}
