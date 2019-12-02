using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ConfigPhaseTwo",menuName="Hackaton SpaceShooter/ConfigPhaseTwo")]
public class ConfigPhaseTwo : ScriptableObject {

	[SerializeField]
	int m_startingLifes = 3;
	public int StartingLifes { get { return m_startingLifes; }}

    [SerializeField]
	int m_scoreWinLife = 100000;
	public int ScoreWinLife { get { return m_scoreWinLife; } }

    [SerializeField]
    float m_timeToFindBoss = 30f;
    public float TimeToFindBoss { get { return m_timeToFindBoss; } }

    [SerializeField]
    float m_bossLife = 5000f;
    public float BossLife { get { return m_bossLife; } }

    [SerializeField]
	string m_initialScene;
	public string InitialScene { get { return m_initialScene; } }

	[SerializeField]
	string m_menuScene;
	public string MenuScene { get { return m_menuScene; } }

    



}
