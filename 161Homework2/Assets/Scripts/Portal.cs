﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (player.transform);
	}

	void OnCollisionEnter(Collision other){
		SoundManager.i.EndSoundAbrupt ("IntroTheme");
		SoundManager.i.EndSoundFade ("AmbientHum", 0.5f);
		SoundManager.i.PlaySound (Sound.SuspenseMusic, 1f);
		Fade.i.FadeOutStart ("main_scene");
		//SceneManager.LoadScene ("main_scene");
	}
}
