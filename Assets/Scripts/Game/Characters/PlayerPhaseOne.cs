using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaseOne : MonoBehaviour {
	PlayerStatePhaseOne playerState;
    public HUDPhaseOne playerPhase;

	[SerializeField] GameObject Shield;
	bool shieldActive;

	[SerializeField] float m_movementForce;
	[SerializeField] float m_rotationSpeed;
	[SerializeField] GameObject m_vacuum;

	PolygonCollider2D m_boxCollider;
	Rigidbody2D m_rigidBody;

	float m_lifes;
	public float Lifes { get { return m_lifes; } }

	bool cleaning = false;
	public bool Cleaning { get { return cleaning; } } 

	// Use this for initialization
	void Start () {
		m_boxCollider = GetComponent<PolygonCollider2D> ();
		m_rigidBody = GetComponent<Rigidbody2D> ();

		m_lifes = 100f;
		shieldActive = false;

		playerState = GameState.Instance.PlayerP1;


	}
	
	// Update is called once per frame
	void Update () {
		/*if (GameState.Paused()) {
			return;
		}*/

		UpdateRotation ();
		UpdateForward ();
		UpdateVacuum ();
		UpdateShield ();
		UpdateSound ();

	}

	void UpdateRotation(){
		if (GameInput.Instance.MoveHorizontal != 0) {
			float rotationSpeed = m_rotationSpeed;
			m_rigidBody.angularVelocity = (rotationSpeed * -GameInput.Instance.MoveHorizontal);
		} else {
			m_rigidBody.angularVelocity = 0f;
		}

	}

	void UpdateForward(){

		if (GameInput.Instance.Shoot) {
			m_rigidBody.AddForce (transform.up * m_movementForce);

		}
	}

	void UpdateVacuum(){
        cleaning = GameInput.Instance.Shoot;
        playerState.setAbsorbing(GameInput.Instance.Shoot);
	}

	public void sustractLife(){
		this.m_lifes = this.m_lifes - 25;
        if(this.m_lifes <= 0) {
            playerPhase.Lose();
        }
	}


	void UpdateShield(){
		Shield.transform.position = transform.position;
		Shield.SetActive (playerState.isShieldActive);
	}

	void UpdateSound (){

	}



}
	