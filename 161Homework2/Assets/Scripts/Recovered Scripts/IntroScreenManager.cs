using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreenManager : MonoBehaviour {


	public GameObject introPanel;
	public GameObject instructionsPanel;
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		Fade.i.FadeInStart();
		SoundManager.i.PlaySoundLoop (Sound.AmbientHum, 1f);

	}
	

	public void CloseIntroPanel(){
		Time.timeScale = 1f;
		instructionsPanel.SetActive (false);
		Fade.i.FadeInStart ();
	}

	public void NextPanel(){
		instructionsPanel.SetActive (true);
		introPanel.SetActive (false);
	}
}
