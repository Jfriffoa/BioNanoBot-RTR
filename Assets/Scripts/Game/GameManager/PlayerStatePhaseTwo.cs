using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePhaseTwo  {

	int m_score;
	public int Score { get { return m_score; } }

	int m_lifes;
	public int Lifes { get { return m_lifes; } }

    //Counter to the lifes earned
    int m_lifeCounter;

	public PlayerStatePhaseTwo()
	{
		m_score = 0;
		m_lifes = GameState.Instance.ConfigP2.StartingLifes;
        m_lifeCounter = 1;
	}

	public void GiveScore(int score)
	{
		m_score += score;
        Debug.Log("Score : " + m_score);
        if (m_score > GameState.Instance.ConfigP2.ScoreWinLife * m_lifeCounter) {
			m_lifes++;
            m_lifeCounter++;
		}
	}

	public void LoseLife()
	{
		m_lifes--;
		Debug.Log ("Lifes : " + m_lifes);

		if (m_lifes == 0) {
			GameState.Instance.EndGame ();
		} else {
			//GameState.Instance.ReloadScene ();
		}
	}

}
