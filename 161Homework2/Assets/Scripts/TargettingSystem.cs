using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TargettingSystem : MonoBehaviour
{
	
    public Sprite icon;


	void Start(){
	}

	//Click to Target
    void OnMouseDown()
    {
		if (gameObject != Player.i.gameObject) {
			UpdateTarget ();
		}
    }

	//Clear target HUD
	public void ClearTarget()
	{
		HUDManager.i.targetHUD.SetActive(false);
		Player.i.target = null;
	}


	void UpdateTarget(){
		Player.i.target = gameObject;
		HUDManager.i.target = gameObject.GetComponent<Unit> ();
		HUDManager.i.targetHUD.SetActive (true);
		HUDManager.i.UpdateHUD ();
		GameObject.FindGameObjectWithTag ("EnemyIcon").GetComponent<Image> ().sprite = icon;
	}

}
