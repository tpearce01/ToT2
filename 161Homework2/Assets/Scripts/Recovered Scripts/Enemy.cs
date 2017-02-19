using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : Unit {

	public Slider healthSlider;

	public override void InitializeAwake(){
		health = maxHealth.baseValue = 3;	
		resource = 0;
	}

	public override void PlayerInput(){
		healthSlider.transform.LookAt (CameraController.i.gameObject.transform);
	}

	public override void Initialize(){
		healthSlider.value = health / maxHealth.Value ();

		//Add this unit to the list of nearby enemies
		Player.i.nearbyEnemies.Add (gameObject);
	}

	public override void Kill(){
		SystemManager.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
		if (Player.i.target = gameObject) {
			HUDManager.i.ClearTarget ();
		}

		Player.i.nearbyEnemies.Remove (gameObject);	//Remove this unit from the list of nearby enemies
		SoundManager.i.EndSoundAbrupt("BatAudio");
		Destroy (gameObject);
	}
		
	public override void ModifyHealth(float value){
		//Modify with variance
		//value *= 1 + Random.Range(-variance, variance);

		if (value < 0)
		{
			//Perform modifiers;
			value *= 1 + damageInModifier;  //Percent damage intake modifier
			value += intDamageInModifier;   //Flat damage intake modifier
		}
		else
		{   //Heal
			value *= 1 + healModifier;  //Percent heal modifier
		}

		//Calculate new health
		health += value;
		if (health <= 0)
		{
			Kill(); //If health drops below 0, unit is dead
		}
		else if (health >= maxHealth.Value())
		{
			health = maxHealth.Value(); //If health exceeds maximum, normalize
		}
		healthSlider.value = health / maxHealth.baseValue;

		HUDManager.i.UpdateHUD ();
		SoundManager.i.PlaySound (Sound.Damaged1, 1f);
		SystemManager.i.SpawnCombatText (gameObject.transform.position, (int)(value * 10));
	}

}
