using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;
	public GameObject startPanel;

	public void HidePanel(){
		startPanel.SetActive (false);
	}

	public void StartGame(){
		//Display load screen
		loadPanel.SetActive(true);
		SoundManager.i.EndSoundFade ("MainMenuTheme", 1f);
		SceneManager.LoadScene ("intro_scene");

	}

	public void QuitButton(){
		Application.Quit();
	}

	void Start(){
		DontDestroyOnLoad (gameObject);

		SoundManager.i.PlaySound (Sound.MainMenuTheme, 1f);
	}
}
