using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitrogeno : MonoBehaviour {

	[SerializeField] GameObject m_toFollowObject;
	[SerializeField] float m_advanceForce;

	Rigidbody2D m_rigidbody;
	float distance;

	PlayerPhaseOne m_player;
	PlayerStatePhaseOne playerState;

	// Use this for initialization
	void Start () {
		this.distance = 1;
		this.m_rigidbody = GetComponent<Rigidbody2D> ();

		this.m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPhaseOne> ();

		playerState = GameState.Instance.PlayerP1;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovimiento ();
	}

	float detectToFollowObjectDistance(){
		Vector3 e_toFollowObjectPosition = m_toFollowObject.transform.position;
		Vector3 e_enemyPosition = transform.position;

		float deltaX = Mathf.Pow(e_enemyPosition.x - e_toFollowObjectPosition.x, 2);
		float deltaY = Mathf.Pow(e_enemyPosition.y - e_toFollowObjectPosition.y, 2);

		float distance = deltaX + deltaY;
		float realDistance = Mathf.Sqrt (distance);

		return realDistance;

	}

	void UpdateMovimiento(){
		float realDistance = detectToFollowObjectDistance ();

		if (realDistance <= 10 && m_player.Cleaning) {
			Vector3 e_toFollowObjectPosition = m_toFollowObject.transform.position;
			Vector3 e_enemyPosition = transform.position;

			Vector3 vectorDistance = e_toFollowObjectPosition - e_enemyPosition;

			Vector3 advanceForce = getForceVector (vectorDistance) * calculate (realDistance);
			m_rigidbody.AddForce (advanceForce);
		}
	}

	Vector3 getForceVector(Vector3 distance){
		float x = 0;
		float y = 0;

		if (distance.x > 0) {
			x = m_advanceForce;
		} else if (distance.x < 0) {
			x = m_advanceForce * (-1);
		}

		if (distance.y > 0) {
			y = m_advanceForce;
		} else if (distance.y < 0) {
			y = m_advanceForce * (-1);
		}

		Vector3 forceVector = new Vector3(x,y);
		return forceVector;

	}

	float calculate(float realDistance){
		float distanceMax = distance * 0.8f;
		float distanceMin = distance * 0.2f;

		if (realDistance > distanceMax) {
			return 0.2f;
		} else if (realDistance < distanceMin) {
			return 1;
		} else {
			return (realDistance * 1) / distanceMax;
		}
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "Vacuum") {
			playerState.DarPuntajePorAbsorber();
            ObjectPool.Kill(gameObject);
		} else if (co.name == "Player") {
			playerState.SustractLifePoints (5);
            //this.e_player.sustractLifes();
            //Debug.Log ("Vidas : " + this.e_player.getLifes);
            ObjectPool.Kill(gameObject);
        }
	}
}