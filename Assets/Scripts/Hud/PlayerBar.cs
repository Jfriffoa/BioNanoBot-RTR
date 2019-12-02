using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour {

    [SerializeField]
    GameObject m_player;

    RectTransform m_transform;

    float originalLifes;
    float originalHeight;

	// Use this for initialization
	void Start () {
        if (m_player == null)
            m_player = GameObject.FindGameObjectWithTag("Player");
        m_transform = GetComponent<RectTransform>();

        if (m_player.GetComponent<PlayerPhaseOne>() != null)
            originalLifes = GameState.Instance.PlayerP1.Lifes;
        else
            originalLifes = GameState.Instance.PlayerP2.Lifes;

        originalHeight = m_transform.sizeDelta.y;
	}
	
	// Update is called once per frame
	void Update () {
        float lifes;
        if (m_player.GetComponent<PlayerPhaseOne>() != null)
            lifes = m_player.GetComponent<PlayerPhaseOne>().Lifes;
        else
            lifes = m_player.GetComponent<PlayerPhaseTwo>().Lifes;

        float y = (lifes / originalLifes) * originalHeight;
        m_transform.sizeDelta = new Vector2(m_transform.sizeDelta.x, y);
	}
}
