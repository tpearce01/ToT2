using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {
	public static MainGameManager i;
	bool looping;
	public bool bossIsDead;
	// Use this for initialization
	void Start () {
		i = this;
		//SoundManager.i.PlaySoundLoop (Sound.SuspenseMusic, 1f);
		Fade.i.FadeInStart ();
	}

	void Update(){
		
		if ((bossIsDead || Player.i.health <= 0) && !SoundManager.i.IsPlaying ("SuspenseMusic") && !SoundManager.i.IsPlaying ("SuspenseLoop")) {
			Fade.i.FadeOutStart ("end_scene");
		} 
		else if (!looping && !SoundManager.i.IsPlaying ("SuspenseMusic")) {
			SoundManager.i.PlaySoundLoop (Sound.SuspenseLoop, 1f);
			looping = true;
		}
	}
	
	
}
