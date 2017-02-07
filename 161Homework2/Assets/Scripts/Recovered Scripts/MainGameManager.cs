using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SoundManager.i.PlaySound (Sound.MainGameTheme, 1f);
	}
	
	
}
