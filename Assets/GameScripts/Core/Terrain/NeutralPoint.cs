using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class NeutralPoint : StrategyPoint {
	public const float controlCheckRad = 10;
	public const float checkInterval = 0.1f;
	public const float captureTime = 10f;
	
	public LayerMask unitMask;
	public Renderer flagRender;
	
	public float captureProgress = 0;
	
	public Force pointHoldForce;
	
	protected Force capturingForce = Force.Neutral;
	
	public void CaptureBy(Force force) {
		this.pointHoldForce = force;
		flagRender.sharedMaterial = TempAssetManager.Instance.forceMat[(int)force];
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
