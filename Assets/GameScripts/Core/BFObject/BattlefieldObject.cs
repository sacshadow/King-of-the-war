using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public abstract class BattlefieldObject : MonoBehaviour {
	public const float scanInterval = 0.1f;
	
	public Force force;
	
	[HeaderAttribute("[物体属性]")]
	public int maxHp = 100;
	public float attackDamage = 25;
	public float attackPerSeconds = 0.75f;
	public float attackRange = 2f;
	public float alertDistance = 10;
	public float defence = 5;
	
	public Func<IEnumerator> Attack;
	public Func<IEnumerator> DefaultState;
	
	public List<BattlefieldObject> target;
	
	public bool isAlive = false;
	
	protected float scanWaitTime = 0;
	
	public virtual void SetForce(Force force) {
		this.force = force;
	}
	
	public virtual void OnPlaceToBattleground() {
		isAlive = true;
		StartCoroutine(BehaviourLoop());
	}
	
	protected virtual IEnumerator BehaviourLoop() {
		
		while(isAlive) {
			
			if(HasTarget())
				yield return StartCoroutine(Attack());
		
			yield return StartCoroutine(DefaultState());
		}
		
	}
	
	protected virtual List<BattlefieldObject> FindTargets() {
		
		return null;
	}
	
	protected bool HasTarget() {
		return target != null && target.Count > 0;
	}
	
	void Update() {
		RefreshTarget();
	}
	
	protected virtual void RefreshTarget() {
		scanWaitTime += Time.deltaTime;
		
		if(scanWaitTime < scanInterval)
			return;
		
		scanWaitTime = 0;
		target = FindTargets();
	}
	
}
