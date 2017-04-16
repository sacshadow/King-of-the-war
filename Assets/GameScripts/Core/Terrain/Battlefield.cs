using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class Battlefield : MonoBehaviour {
	public static Battlefield Instance;
	
	public LayerMask groundMask;
	
	public Transform[] camPoint;
	
	public Camp[] camp;
	public NeutralPoint[] sidePoint;
	public NeutralPoint midPoint;
	
	public Road[] leftRoad;
	public Road[] rightRoad;
	public Road[] midRoad;
	public Road[] extendRoad;
	
	
	void Awake() {
		Instance = this;
		Pathfind.bf = this;
		Pathfind.Init();
	}
}
