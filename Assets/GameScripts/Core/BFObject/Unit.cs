using UnityEngine;
using UnityEngine.AI;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public abstract class Unit : BattlefieldObject {
	public const float targetPointCloseDistance = 2f;
	public const float moveDisTolerance = 0.1f;
	
	
	[HeaderAttribute("[单位属性]")]
	public float moveSpeed = 5;
	
	protected StrategyPoint strategyPoint;
	
	
	public override void SetForce(Force force) {
		base.SetForce(force);
		GetComponent<Renderer>().sharedMaterial = TempAssetManager.Instance.forceMat[(int)force];
	}
	
	public virtual float GetMoveSpeed() {
		return moveSpeed;
	}
	
	protected virtual void Awake() {
		FindNextMovePoint();
		DefaultState = MoveToStrategyPoint;
	}
	
	protected virtual void FindNextMovePoint() {
		strategyPoint = Pathfind.GetNextWaypoint(force,GetStandPlaceName(), transform.position);
		// Debug.Log(strategyPoint.name);
	}
	
	protected string GetStandPlaceName() {
		RaycastHit hit;
		if(Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, Battlefield.Instance.groundMask))
			return hit.collider.name;
		throw new Exception("unit not stand in legal position");
	}
	
	protected IEnumerator MoveToStrategyPoint() {
		var path = new NavMeshPath();
		
		if(!NavMesh.CalculatePath(transform.position, strategyPoint.transform.position, NavMesh.AllAreas, path))
			throw new Exception("path not find");
		
		int index= 0;
		while(index < path.corners.Length && !HasTarget() && !IsCloseToTargetPoint()) {
			yield return StartCoroutine(MoveToPoint(path.corners[index]));
			index++;
		}
		
		FindNextMovePoint();
	}
	
	protected virtual IEnumerator MoveToPoint(Vector3 point) {
		
		while(!HasTarget() && !IsCloseTo(point) && !IsCloseToTargetPoint()) {
			transform.position = Vector3.MoveTowards(transform.position, point, GetMoveSpeed() * Time.deltaTime);
			yield return null;
		}
	}
	
	protected bool IsCloseTo(Vector3 point) {
		return Vector3.Distance(transform.position, point) < moveDisTolerance;
	}
	
	protected virtual bool IsCloseToTargetPoint() {
		// return strategyPoint.name == GetStandPlaceName();
		return Vector3.Distance(transform.position, strategyPoint.transform.position) < targetPointCloseDistance;
	}
	
}
