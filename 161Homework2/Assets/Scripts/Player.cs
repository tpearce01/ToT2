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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ts.ClearTarget();
        }

		//Tab Target
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (nearbyEnemies.Count > 1) {						//If there are multiple enemies nearby
				if (target != null) {							//And we have a target
					startIndex = nearbyEnemies.IndexOf (target);//Start index is the target's index
				} else {										//Otherwise
					startIndex = 0;								//Start at index 0
				}
				targetIndex =  startIndex + 1;					//Target index starts at index + 1
			} else {
				if (nearbyEnemies.Count == 0) {
					return;
				}//If there is only 1 enemy
				if(nearbyEnemies[0].GetComponent<Renderer>().isVisible){
					targetIndex = startIndex = 0;								//Set index to 0
					target = nearbyEnemies [0];									//And make the target the only enemy
					UpdateTarget ();											//And update target
					return;														//Then exit function
				}
			}

			//Continue if there is more than 1 enemy
			int counter = nearbyEnemies.Count;
			for (;targetIndex != startIndex; targetIndex++) {	//while target index != start index
				if (targetIndex > nearbyEnemies.Count - 1) {	//if targetIndex exceeds the number of enemies
					targetIndex = 0;							//Reset the index to 0
				}

				//If the player is not already targeting the enemy at targetIndex
				if (target != nearbyEnemies [targetIndex]) {
					if(nearbyEnemies[targetIndex].GetComponent<Renderer>().isVisible){
						target = nearbyEnemies [targetIndex];	//Set that enemy as the target
						UpdateTarget ();						//then update the target
						return;									//Then exit function
					}
				}
				counter--;
				if (counter == 0) {
					break;
				}
			}
		}

		if (target != null && target != gameObject) {
			if (Input.GetKeyDown (KeyCode.Alpha1) && !muteAction) {
				StartCast (1.5f, 1, 5, Cast.BaseCast);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) && !muteAction && resource >= 10) {
				InstantCast (2, -10);
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
		SoundManager.i.EndAll ("BatAudio");
		Player.i.nearbyEnemies.Remove (gameObject);
		if (SoundManager.i.IsPlaying ("SuspenseMusic")) {
			SoundManager.i.EndSoundFade ("SuspenseMusic", 5f);
		} else {
			SoundManager.i.EndSoundFade ("SuspenseLoop", 5f);
		}
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
		SoundManager.i.EndSoundAbrupt ("Casting");
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
		SoundManager.i.PlaySound (Sound.CastSuccess, 0.1f);
		SoundManager.i.EndSoundAbrupt ("Casting");
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
