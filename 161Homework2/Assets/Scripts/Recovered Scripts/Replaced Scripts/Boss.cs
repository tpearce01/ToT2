using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit {

	public static Boss i;
	public Slider healthSlider;
	public GameObject bossObject;

	public override void InitializeAwake(){
		i = this;
		health = 100;
		resource = 0;
	}

	public override void PlayerInput(){
		//hold
		if (health < 80 && BossBehavior.i.phase == 1) {
			BossBehavior.i.phase = 2;
		} else if (health < 35 && BossBehavior.i.phase == 2) {
			BossBehavior.i.phase = 3;
		}

		healthSlider.transform.LookAt (CameraController.i.gameObject.transform);
	}

	public override void Initialize(){
		healthSlider.value = health / maxHealth.Value ();
		Player.i.nearbyEnemies.Add (gameObject);
	}

	public override void Kill(){
		SystemManager.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = temp.Length - 1; i > -1; i--) {
			Destroy (temp [i]);
		}
		Player.i.nearbyEnemies.Remove (gameObject);
		if (SoundManager.i.IsPlaying ("SuspenseMusic")) {
			SoundManager.i.EndSoundFade ("SuspenseMusic", 5f);
		} else {
			SoundManager.i.EndSoundFade ("SuspenseLoop", 5f);
		}
		MainGameManager.i.bossIsDead = true;
		Destroy (bossObject);
	}

	public override void ModifyHealth(float value){
		//Modify with variance
		value *= 1 + Random.Range(-variance, variance);

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
		SystemManager.i.SpawnCombatText (gameObject.transform.position, (int)(value * 10));
	}
}
