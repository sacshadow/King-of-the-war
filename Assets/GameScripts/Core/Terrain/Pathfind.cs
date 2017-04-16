using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Pathfind {
	public const int a = 0, b = 1, c = 2;
	public static Battlefield bf;
	
	public static Dictionary<string, Func<Vector3, StrategyPoint>> forceA;
	public static Dictionary<string, Func<Vector3, StrategyPoint>> forceB;
	public static Dictionary<string, Func<Vector3, StrategyPoint>> forceC;
	
	public static StrategyPoint GetNextWaypoint(Force force, string standPositionName, Vector3 position) {
		try{
		if(force == Force.A)
			return forceA[standPositionName](position);
		if(force == Force.B)
			return forceB[standPositionName](position);
		if(force == Force.C)
			return forceC[standPositionName](position);
		}
		catch(Exception e) {
			Debug.LogError("GetNextWaypoint " + force + " " + standPositionName + " " + position);
			throw e;
		}
		throw new System.Exception("Unknow force " + force);
	}
	
	
	public static void Init() {
		SetForceA();
		SetForceB();
		SetForceC();
	}
	
	public static Func<Vector3, StrategyPoint> CloseTo(params StrategyPoint[] point) {
		return x=> point[GetMinimalIndex(point.Select(y=>Vector3.Distance(x,y.transform.position)).ToArray())];
	}
	
	public static int GetMinimalIndex(float[] dis) {
		int rt = 0;
		for(int i =1; i< dis.Length; i++) rt = dis[rt] < dis[i] ? rt : i;
		return rt;
	}
	
	public static Func<Vector3, StrategyPoint> Rand(StrategyPoint lhs, StrategyPoint rhs) {
		var pick = new bool[]{false,true,false,true,};
		var list = new List<bool>(pick);
		
		Func<bool> GetDir = ()=> {
			var draw = UnityEngine.Random.Range(0,100) % list.Count;
			var rt = list[draw];
			list.RemoveAt(draw);
			if(list.Count == 0) list.AddRange(pick);
			return rt;
		};
		return p=> GetDir() ? Order(lhs,rhs)(p) : Order(rhs,lhs)(p);
	}
	 
	public static Func<Vector3, StrategyPoint> Order(StrategyPoint first, StrategyPoint second) {
		return x=> first.isFortressExist ? first : second;
	}
	 
	public static void SetForceA() {
		forceA = new Dictionary<string, Func<Vector3, StrategyPoint>> {
			{"Camp_A", CloseTo(bf.sidePoint[c], bf.midPoint, bf.sidePoint[b])},
			{"Camp_B", x=>bf.camp[c]},
			{"Camp_C", x=>bf.camp[b]},
			
			{"NeutralPoint_A", Rand(bf.camp[c], bf.camp[b])},
			{"NeutralPoint_B", x=>bf.camp[c]},
			{"NeutralPoint_C", x=>bf.camp[b]},
			{"NeutralPoint_D", x=>bf.sidePoint[a]},
			
			{"Road_LA", x=>bf.sidePoint[c]},
			{"Road_LB", Order(bf.camp[b], bf.camp[c])},
			{"Road_LC", x=>bf.camp[c]},
			
			{"Road_MA", x=>bf.midPoint},
			{"Road_MB", x=>bf.camp[b]},
			{"Road_MC", x=>bf.camp[c]},
			
			{"Road_RA", x=>bf.sidePoint[b]},
			{"Road_RB", x=>bf.camp[b]},
			{"Road_RC", Order(bf.camp[c], bf.camp[b])},
			
			{"Road_EA", x=>bf.sidePoint[a]},
			{"Road_EB", x=>bf.camp[b]},
			{"Road_EC", x=>bf.camp[c]},
		};
	}
	
	public static void SetForceB() {
		forceB = new Dictionary<string, Func<Vector3, StrategyPoint>> {
			{"Camp_A", x=>bf.camp[c]},
			{"Camp_B", CloseTo(bf.sidePoint[a], bf.midPoint, bf.sidePoint[c])},
			{"Camp_C", x=>bf.camp[a]},
			
			{"NeutralPoint_A", x=>bf.camp[c]},
			{"NeutralPoint_B", Rand(bf.camp[a], bf.camp[c])},
			{"NeutralPoint_C", x=>bf.camp[a]},
			{"NeutralPoint_D", x=>bf.sidePoint[b]},
			
			{"Road_LA", x=>bf.camp[a]},
			{"Road_LB", x=>bf.sidePoint[a]},
			{"Road_LC",  Order(bf.camp[c], bf.camp[a])},
			
			{"Road_MA", x=>bf.camp[a]},
			{"Road_MB", x=>bf.midPoint},
			{"Road_MC", x=>bf.camp[c]},
			
			{"Road_RA", Order(bf.camp[a], bf.camp[c])},
			{"Road_RB", x=>bf.sidePoint[c]},
			{"Road_RC", x=>bf.camp[c]},
			
			{"Road_EA", x=>bf.camp[a]},
			{"Road_EB", x=>bf.sidePoint[b]},
			{"Road_EC", x=>bf.camp[c]},
		};
	}
	
	public static void SetForceC() {
		forceC = new Dictionary<string, Func<Vector3, StrategyPoint>> {
			{"Camp_A", x=>bf.camp[b]},
			{"Camp_B", x=>bf.camp[a]},
			{"Camp_C", CloseTo(bf.sidePoint[a], bf.midPoint, bf.sidePoint[b])},
			
			{"NeutralPoint_A", x=>bf.camp[b]},
			{"NeutralPoint_B", x=>bf.camp[a]},
			{"NeutralPoint_C", Rand(bf.camp[a], bf.camp[b])},
			{"NeutralPoint_D", x=>bf.sidePoint[c]},
			
			{"Road_LA", Order(bf.camp[a], bf.camp[b])},
			{"Road_LB", x=>bf.camp[b]},
			{"Road_LC", x=>bf.sidePoint[b]},
			
			{"Road_MA", x=>bf.camp[a]},
			{"Road_MB", x=>bf.camp[b]},
			{"Road_MC", x=>bf.midPoint},
			
			{"Road_RA", x=>bf.camp[a]},
			{"Road_RB", Order(bf.camp[b], bf.camp[a])},
			{"Road_RC", x=>bf.sidePoint[a]},
			
			{"Road_EA", x=>bf.camp[a]},
			{"Road_EB", x=>bf.camp[b]},
			{"Road_EC", x=>bf.sidePoint[c]},
		};
	}
	
	
}
