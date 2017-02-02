using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * EnemySpawnAttack1
 * 
 * When the boss summons adds, the adds will attack at regular intervals,
 * spawning gameobjects with this script attached
 * 
 * This script will move the object towards the player at the specified speed,
 * then damage the player on collision and destroy itself.
 */ 
public class EnemySpawnAttack1 : MonoBehaviour {

	public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, Player.i.transform.position, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.tag);
		if(other.gameObject.CompareTag("Player")){
			Player.i.ModifyHealth (-3);
			Destroy (gameObject);
		}
	}
}
