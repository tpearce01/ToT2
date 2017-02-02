using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
	//Static reference to movement controller
	public static MovementController i;

	//Animation vars
	public Animator animations;
	public GameObject model;
	bool test = false;

	//Physics vars
    public float moveSpeed;
    public Rigidbody rigidbody;
	bool isMoving = false;

	//Other vars
	public bool isDead = false;
	public bool muteMovement = false;


	void Awake(){
		i = this;
	}

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
		if (!isDead && !muteMovement) {
			//Get user input and move player
			MovePlayer ();
		}
	}

	public void Kill(){
		isDead = true;
		muteMovement = true;
		animations.SetBool ("Death", true);
	}


	//Get player input and move player is applicable
    public void MovePlayer()
    {
		//Is the player moving?
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {
			isMoving = true;
			animations.SetFloat ("Speed", 1);
			Player.i.BreakCast ();
		}else {
			isMoving = false;
			animations.SetFloat ("Speed", 0);
		}

			//Move forward
			if (Input.GetKey (KeyCode.W) || (Input.GetMouseButton (0) && Input.GetMouseButton (1))) {
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
					rigidbody.MovePosition (rigidbody.transform.position + transform.forward * moveSpeed * Time.deltaTime * 0.7f);

				} else {
					rigidbody.MovePosition (rigidbody.transform.position + transform.forward * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,0,0));

				}
			}

			//Move backward
			if (Input.GetKey (KeyCode.S)) {
				rigidbody.MovePosition (rigidbody.transform.position - transform.forward * moveSpeed * Time.deltaTime * 0.7f);
				model.transform.localRotation = Quaternion.Euler (new Vector3 (0,180,0));

			}

			//Move left
			if (Input.GetKey (KeyCode.A)) {
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
					if (Input.GetKey (KeyCode.W)) {
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f + transform.forward* moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-45,0));
					}
					else if(Input.GetKey(KeyCode.S)){
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f - transform.forward* moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-135,0));
					}
					else{
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-90,0));

					}
				} else {
					rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-90,0));

				}
			}

			//Move right
			if (Input.GetKey (KeyCode.D)) {
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
					if (Input.GetKey (KeyCode.W)) {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f + transform.forward * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,45,0));

					} else if (Input.GetKey (KeyCode.S)) {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f - transform.forward * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,135,0));

					} else {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,90,0));

					}
				} else {
					rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,90,0));

				}
			}

    }

    
}
