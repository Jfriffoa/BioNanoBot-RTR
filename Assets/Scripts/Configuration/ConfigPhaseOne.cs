using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ConfigPhaseOne",menuName="Hackaton SpaceShooter/ConfigPhaseOne")]
public class ConfigPhaseOne : ScriptableObject {

	[SerializeField]
	int m_initialLifePoints = 100;
	public int InitialLifePoints { get { return m_initialLifePoints; }}

	[SerializeField]
	int m_puntajeMaxBarraDeLimpieza = 100;
	public int PuntajeMaxBarraDeLimpieza { get { return m_puntajeMaxBarraDeLimpieza; }}

	[SerializeField]
	int m_puntajeDeLimpieza = 5;
	public int PuntajeDeLimpieza { get { return m_puntajeDeLimpieza; } }

	[SerializeField]
	string m_escenaMenu;
	public string EscenaMenu { get { return m_escenaMenu; } }

	[SerializeField]
	string m_escenaExploracion;
	public string InitialScene { get { return m_escenaExploracion; } }

	[SerializeField]
	string m_escenaSpaceShooter;
	public string EscenaSpaceShooter { get { return m_escenaSpaceShooter; } }

	[SerializeField]
	string m_escenaNarrativa;
	public string EscenaNarrativa { get { return m_escenaNarrativa; } }



}
