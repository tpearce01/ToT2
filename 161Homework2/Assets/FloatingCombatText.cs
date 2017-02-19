using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingCombatText : MonoBehaviour {

	public TextMesh damage;
	float timeToLive;

	void Start(){
		timeToLive = 2;
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
	}

	void Update(){
		if (timeToLive > 0) {
			timeToLive -= Time.deltaTime;
		} else {
			Destroy (gameObject);
		}

		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 1*Time.deltaTime, gameObject.transform.position.z);

		gameObject.transform.LookAt (CameraController.i.gameObject.transform);
		gameObject.transform.Rotate (0, 180, 0);
	}

	public void Initialize(int value){
		damage.text = value.ToString ();
	}

	public void Initialize(int value, Color c){
		Initialize (value);
		damage.color = c;
	}
}
