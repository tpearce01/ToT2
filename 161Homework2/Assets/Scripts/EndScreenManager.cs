using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour {

	public Text victoryTitle;
	public Text victoryText;

	// Use this for initialization
	void Start () {
		Fade.i.FadeInStart ();
		if (!MainGameManager.i.bossIsDead) {
			victoryTitle.text = "You Died";
			victoryText.text = "You have failed to destroy the crystal which binds Xalac to our mortal world. " +
			"The spirits call to you, instilling you with the power to rewind time a short while, to before " +
			"your demise. What will you do?";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart(){
		Fade.i.FadeOutStart("intro_scene");
	}

	public void Quit(){
		Application.Quit();
	}
}
