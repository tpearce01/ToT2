using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour {

	public int speed;
	public GameObject castTarget;

	void Start(){
		
		castTarget = Player.i.castTarget;
	}

	// Update is called once per frame
	void Update () {
		if (castTarget != null) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, castTarget.transform.position, speed * Time.deltaTime);
		} else {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Boss") || other.CompareTag("Enemy")) {
			castTarget.GetComponent<Unit>().ModifyHealth (-2);
			SystemManager.i.SpawnObject (Prefab.SmallExplosion, gameObject.transform.position);
			Destroy (gameObject);
		}

	}
}
