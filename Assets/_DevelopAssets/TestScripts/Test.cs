using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class Test : MonoBehaviour {
	
	public Solider solider_perfab;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0))
			CheckMouseHit();
		
	}
	
	private void CheckMouseHit() {
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)) {
			// Debug.Log(Pathfind.GetNextWaypoint(Force.A, hit.collider.name, hit.point).name);
			((Solider)Instantiate(solider_perfab, hit.point, Quaternion.identity)).OnPlaceToBattleground();
		}
		
	}
}
