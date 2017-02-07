using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReturnToIdle(){
		anim.SetBool ("Attack", false);
	}

	public void Die(){
		anim.SetBool ("Death", true);
	}
}
