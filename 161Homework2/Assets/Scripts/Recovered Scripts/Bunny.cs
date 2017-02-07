using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour {

	float startTimer;

	// Use this for initialization
	void Start () {
		startTimer = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer > 0) {
			startTimer -= Time.deltaTime;
		} else {
			//gameObject.
		}
	}
}
