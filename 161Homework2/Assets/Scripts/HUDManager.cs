using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	//Static reference
	public static HUDManager i;

	//Player
	public GameObject playerHUD;
	public Slider playerHP;
	public Slider playerResource;

	//Cast
	public bool isCasting;
	public GameObject castUI;
	public Slider castBar;
	public Text castText;
	public float totalCastTime;
	public float castTimer;
	Cast castType;

	//Target
	public GameObject targetHUD;
	public Unit target;
	public Slider targetHP;
	public Slider targetResource;

	// Use this for initialization
	void Start () {
		i = this;
		targetHUD.SetActive (false);
		UpdateHUD ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isCasting) {
			UpdateCast ();
		}
	}

	//Update resource sliders
	public void UpdateHUD(){
		playerHP.value = Player.i.health / Player.i.maxHealth.baseValue;
		playerResource.value = Player.i.resource / Player.i.maxResource.baseValue;

		if (target != null) {
			targetHP.value = target.health / target.maxHealth.baseValue;
			targetResource.value = target.resource / target.maxResource.baseValue;
		} 
	}

	//Begin casting & show UI
	public void StartCast(float time, Cast type){
		if (!isCasting) {
			castUI.SetActive (true);
			castTimer = 0f;
			totalCastTime = time;
			isCasting = true;
			castType = type;
		}
	}

	//Update cast UI
	public void UpdateCast(){
		castTimer += Time.deltaTime;
		castText.text = castTimer.ToString ("F1") + " / " + totalCastTime.ToString ("F1");
		castBar.value = (float)castTimer / totalCastTime;


		if (castTimer >= totalCastTime) {
			CastSuccess ();
		}
	}

	//Finish casting
	public void CastSuccess(){
		//tell player
		Player.i.CastSuccess(castType);
		BreakCast ();
	}

	public void BreakCast(){
		totalCastTime = (float)int.MaxValue;
		isCasting = false;
		castUI.SetActive (false);
	}

	//Clear target HUD
	public void ClearTarget()
	{
		targetHUD.SetActive(false);
		Player.i.target = null;
	}
}
