using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviourSingleton<GameInput> {

    float m_moveHorizontal;
    float m_moveVertical;
    bool m_shoot;
    bool byTrigger;

    bool m_pause;
    bool m_next = false;
    bool m_back = false;

    bool m_up = false;
    bool m_down = false;

    float lastVerticalValue = 0;

	public float MoveHorizontal { get { return m_moveHorizontal; } }
    public float MoveVertical { get { return m_moveVertical; } }
    public bool Shoot { get { return m_shoot; } }
    public bool Pause { get { return m_pause; } }
    public bool Next { get { return m_next; } }
    public bool Back { get { return m_back; } }

    public bool Up { get { return m_up; } }
    public bool Down { get { return m_down; } }

    public bool PauseFunction = true;

    void Update () {
        m_moveHorizontal = Input.GetAxis("Horizontal");
        m_moveVertical = Input.GetAxis("Vertical");

        //Mantain the boolean as true until the player stop pressing the button
        if (Input.GetButtonDown("Shoot"))
            m_shoot = true;
        else if (Input.GetButtonUp("Shoot"))
            m_shoot = false;

        if (Input.GetAxis("Trigger") < 0) {
            m_shoot = true;
            byTrigger = true;
        } else {
            if (byTrigger) {
                m_shoot = false;
            }
            byTrigger = false;
        }

        //Mantain the boolean true only in the first press of the button
        if (Input.GetButtonDown("Back"))
            m_back = true;
        else
            m_back = false;


        if (Input.GetButtonDown("Pause")) {
            if (PauseFunction)
                ChangePause();

            m_pause = true;
        } else {
            m_pause = false;
        }

        if (Input.GetButtonDown("Next")) {
            m_next = true;
        } else {
            m_next = false;
        }

        //Transform the Y-Axis to a Up/Down booleans
        if (m_moveVertical != 0) {
            if (m_moveVertical < 0)
                m_up = !SameSign(m_moveVertical, lastVerticalValue);
            else if (m_moveVertical > 0)
                m_down = !SameSign(m_moveVertical, lastVerticalValue);
            else
                m_up = m_down = false;
        }

        lastVerticalValue = m_moveVertical;
    }

    public void ChangePause() {

        if (GameState.Paused())
            GameManager.Instance.Despause();
        else
            GameManager.Instance.Pause();

        GameState.Instance.ChangePause();

        //Debug.Log(GameState.Paused());
    }

    bool SameSign(float n1, float n2) {
        return (n1 < 0 && n2 < 0) || (n1 > 0 && n2 > 0);
    }
}
