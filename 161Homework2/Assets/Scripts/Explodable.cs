using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Explodable : MonoBehaviour {

	public Slider timeSlider;
	public float explosionTimer;
	float defaultExplosionTime;
	public Collider col;


	bool playerInRange;

	void Awake(){
		playerInRange = false;
	}

	void Start(){
		defaultExplosionTime = 2f;
		explosionTimer = defaultExplosionTime;
	}

	void Update () {
		explosionTimer -= Time.deltaTime;
		if (explosionTimer <= 0) {
			if (col.enabled == false) {
				col.enabled = true;
			} else {
				Explode ();
			}
		} else {
			timeSlider.value = explosionTimer / defaultExplosionTime;
		}
		timeSlider.transform.LookAt (CameraController.i.gameObject.transform);
	}

	public void Explode(){
		SystemManager.i.SpawnObject (Prefab.Explosion, gameObject.transform.position);
		if (playerInRange) {
			Player.i.ModifyHealth (-30);
		}
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){
			playerInRange = true;
		}
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.CompareTag("Player")){
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			playerInRange = false;
		}
	}

}
