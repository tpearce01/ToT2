using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Player : Unit
{
    public static Player i;
    public GameObject target = null;
	public GameObject castTarget = null;
    public GameObject targetHUD;
    private TargettingSystem ts;

	//Input Management
	public bool muteAction = false;

	//Animations
	public Animator animations;
	public bool castAnimation = false;
	public GameObject model;

	//Resource Controls
	float damage;
	float resourceRegen;

	//Target assist vars
	public List<GameObject> nearbyEnemies = new List<GameObject>();	//List of nearby enemies for tab targeting
	int targetIndex = 0;
	int startIndex;


    public override void PlayerInput()
    {
        //not implemented
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ts.ClearTarget();
        }

		//Tab Target
		if (Input.GetKeyDown (KeyCode.Tab)) {
			//Search through list of nearby enemies
			Debug.Log(nearbyEnemies);
			if (nearbyEnemies.Count > 1) {
				startIndex = nearbyEnemies.IndexOf (target);
				targetIndex =  startIndex + 1;
			} else {
				targetIndex = startIndex = 0;
				target = nearbyEnemies [0];
				UpdateTarget ();
			}

			for (;targetIndex != startIndex; targetIndex++) {
				//if targetIndex goes out of bounds, loop it back to 0
				if (targetIndex > nearbyEnemies.Count - 1) {
					targetIndex = 0;
				}

				//If not already targetting the enemy at targetIndex, target that enemy & break loop
				if (target != nearbyEnemies [targetIndex]/* && nearbyEnemies[targetIndex].renderer.isVisible*/) {
					//set new target
					target = nearbyEnemies [targetIndex];
					UpdateTarget ();
					break;
				}
			}
		}

		if (target != null && target != gameObject) {
			if (Input.GetKeyDown (KeyCode.Alpha1) && !muteAction) {
				StartCast (1.5f, 1, 5, Cast.BaseCast);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) && !muteAction && resource >= 10) {
				InstantCast (3, -10);
			}
		}
    }

	public override void InitializeAwake(){
		i = this;
		health = 100;
		resource = 0;
	}

    public override void Initialize()
    {
        ts = gameObject.GetComponent<TargettingSystem>();
    }

	public override void Kill(){
		muteAction = true;
		BreakCast ();
		MovementController.i.Kill ();
	}

	public void StartCast(float time, float d, float regen, Cast type){
		Vector3 targetLook = new Vector3 (target.transform.position.x, model.transform.position.y, target.transform.position.z);
		model.transform.LookAt (targetLook);
		castTarget = target;
		damage = d;
		resourceRegen = regen;
		animations.SetBool ("Casting", true);
		if (type == Cast.InstantCast) {
			CastSuccess (Cast.InstantCast);
		} else {
			HUDManager.i.StartCast (time, type);
		}
	}

	/*
	public void CastSuccess(){
		DamageEnemy (damage);
		ModifyResource (resourceRegen);
		BreakCast ();
	}*/

	public void BreakCast(){
		HUDManager.i.BreakCast ();
		animations.SetBool ("Casting", false);
	}

	public void DamageEnemy(float value){
		if (target.GetComponent<Unit>() != null) {
			target.GetComponent<Unit> ().ModifyHealth (-value);
		}
	}

	public void InstantCast(float d, float regen){
		if (!HUDManager.i.isCasting) {
			animations.SetBool ("Casting", true);
			damage = d;
			resourceRegen = regen;
			CastSuccess (Cast.InstantCast);
		}
	}

	public void CastSuccess(Cast type){
		animations.SetBool ("Casting", false);
		ModifyResource (resourceRegen);
		switch ((int)type) {
			case 0:
				//base cast
				SystemManager.i.SpawnObject (Prefab.BaseCast, gameObject.transform.position);
				break;
		case 1:
				//Instant cast
			DamageEnemy (damage);
				break;
			default:
				Debug.Log ("Error on Cast");
				break;
		}
	}


	void UpdateTarget(){
		if (target != null) {
			HUDManager.i.target = target.GetComponent<Unit> ();	//Update HUD 
			HUDManager.i.targetHUD.SetActive (true);
			HUDManager.i.UpdateHUD ();
			GameObject.FindGameObjectWithTag ("EnemyIcon").GetComponent<Image> ().sprite = target.GetComponent<TargettingSystem> ().icon;
		} 
	}
}

public enum Cast{
	BaseCast = 0,
	InstantCast = 1,
	StrongCast = 2
}
