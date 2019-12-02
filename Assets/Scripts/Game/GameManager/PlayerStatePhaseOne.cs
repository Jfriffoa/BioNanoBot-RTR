using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePhaseOne {

	float porcentajeR;
	public float getPorcentajeR { get { return porcentajeR; } }

	float e_cleaningScore;
	public float getCleaningScore { get { return e_cleaningScore; } }

	int e_lifePoints;
	public int Lifes { get { return e_lifePoints; } }

	bool absorbing;
	public bool isAbsorbing { get { return absorbing; } }

	bool shieldActive;
	public bool isShieldActive { get { return shieldActive; } }
	float shieldStartTime;

	public void setAbsorbing (bool absorbing){
		this.absorbing = absorbing;
	}

	public PlayerStatePhaseOne()
	{
		e_cleaningScore = 0;
		e_lifePoints = GameState.Instance.ConfigP1.InitialLifePoints;

		porcentajeR = 0f;

		shieldStartTime = Time.time;

	}


	public void DarPuntajePorAbsorber(){
		DarPuntaje (GameState.Instance.ConfigP1.PuntajeDeLimpieza);
		UpdatePorcentajeR (GameState.Instance.ConfigP1.PuntajeDeLimpieza);
	}

	public void DarPuntajePorAbsorber(int bonus){
		DarPuntaje (bonus);
		UpdatePorcentajeR (bonus);

	}

	public void DarPuntaje(int puntaje)
	{
		e_cleaningScore += puntaje;
		Debug.Log ("punaje : " + e_cleaningScore);

	}

	public void PerderVida()
	{
		e_lifePoints = (int)(e_lifePoints - (e_lifePoints*0.25f)) ;

		if (e_lifePoints <= 0) {
			GameState.Instance.EndGame ();
		} else {
			GameState.Instance.ReloadScene();
		}
	}

	public void SustractLifePoints(int damage){
		e_lifePoints = e_lifePoints - damage;
	}

	void UpdatePorcentajeR(int porcentaje){
		porcentajeR += porcentaje;

		if (porcentajeR >= 100f) {
			porcentajeR = 0f;
			shieldActive = true;

		} else {
			UpdateShieldState ();

		}
	}

	void UpdateShieldState(){
		if (Time.time - shieldStartTime >= 10) {
			shieldActive = false;
			shieldStartTime = Time.time - shieldStartTime;
		}
	}

}
