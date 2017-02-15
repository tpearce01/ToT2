using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn1 : MonoBehaviour {

	float attackTimer;
	public float timeBetweenAttacks;
	public GameObject model;

	void Start(){
		attackTimer = timeBetweenAttacks;
		SoundManager.i.PlaySoundLoop (Sound.BatAudio, 0.2f);
	}

	// Update is called once per frame
	void Update () {
		if (Player.i.health > 0) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				SystemManager.i.SpawnObject (Prefab.EnemySpawnCast1, gameObject.transform.position);
				attackTimer = timeBetweenAttacks;
			}
			model.transform.LookAt (Player.i.transform);
		}
	}

	void OnDestroy(){
		SoundManager.i.EndSoundAbrupt ("BatAudio");
	}
}
