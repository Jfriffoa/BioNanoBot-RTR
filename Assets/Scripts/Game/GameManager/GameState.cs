using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState{

	static GameState m_game;
	public static GameState Instance { get { return m_game; } }

	PlayerStatePhaseOne m_playerP1;
    public PlayerStatePhaseOne PlayerP1 { get { return m_playerP1; } }

    PlayerStatePhaseTwo m_playerP2;
    public PlayerStatePhaseTwo PlayerP2 { get { return m_playerP2; } }

    ConfigPhaseOne m_configP1;
    public ConfigPhaseOne ConfigP1 { get { return m_configP1; } }

    ConfigPhaseTwo m_configP2;
	public ConfigPhaseTwo ConfigP2 { get { return m_configP2; } }

	enum State {
		StartingScene,
        Tutorial,
		Playing,
        Paused,
		LosingLife,
		LosingGame,
		LoadingScene
	}
	State m_state;

	string m_lastScene;

	public static void CreateGame(ConfigPhaseOne configP1, ConfigPhaseTwo configP2, int i)
	{
		m_game = new GameState (configP1, configP2);
		m_game.Starting (i);
	}

	GameState(ConfigPhaseOne configP1, ConfigPhaseTwo configP2)
	{
        m_configP1 = configP1;
		m_configP2 = configP2;
	}

	public void Starting(int i)
	{
        Debug.Log("STARTING ");
        m_playerP1 = new PlayerStatePhaseOne();
        m_playerP2 = new PlayerStatePhaseTwo();

        if (i == 1)
            LoadScene(m_configP1.InitialScene);
        else
            LoadScene(m_configP2.InitialScene);
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
		m_lastScene = scene;
	}

	public void ReloadScene()
	{
		LoadScene (m_lastScene);
	}

	public void EndGame()
	{
		m_game = null;
		SceneManager.LoadScene (m_configP2.MenuScene);
	}

    public static bool Paused() {
        return Instance.m_state == State.Paused;
    }

    public static bool Tutorial() {
        return Instance.m_state == State.Tutorial;
    }

    public static bool Playing() {
        return Instance.m_state == State.Playing;
    }
    
    public static void Play() {
        Instance.m_state = State.Playing;
    }

    public void InTutorial() {
        m_state = State.Tutorial;
    }

    public void ChangePause() {
        if (m_state == State.Paused) {
            m_state = State.Playing;
            Debug.Log("Playing");
        } else {
            m_state = State.Paused;
            Debug.Log("PAUSED");
            Debug.Log(m_state);
        }
    }

    public void Pause() {
        m_state = State.Paused;
    }
}
