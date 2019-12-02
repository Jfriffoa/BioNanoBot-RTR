using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    [SerializeField]
    GameObject m_toFollow;

    [SerializeField]
    float m_speed = 1.0f;

    [SerializeField]
    float m_offset = 0f;

    [SerializeField]
    bool m_followUp;

    [SerializeField]
    bool m_followLeft;

    [SerializeField]
    bool m_followRight;

    [SerializeField]
    bool m_followDown;

    void Start() {
        m_toFollow = GameObject.Find(m_toFollow.name);
    }

    void Update () {
        if (GameState.Paused())
            return;

        UpdateFollow();
	}

    void UpdateFollow() {
        if (m_toFollow == null)
            return;

        Vector2 objPos = m_toFollow.transform.position;
        Vector2 pos = transform.position;

        if (m_followUp && objPos.y > pos.y + m_offset) {
            pos += Vector2.up * m_speed * Time.deltaTime;
        }

        if (m_followLeft && objPos.x < pos.x - m_offset) {
            pos += Vector2.left * m_speed * Time.deltaTime;
        }

        if (m_followRight && objPos.x > pos.x + m_offset) {
            pos += Vector2.right * m_speed * Time.deltaTime;
        }

        if (m_followDown && objPos.y < pos.y - m_offset) {
            pos += Vector2.down * m_speed * Time.deltaTime;
        }

        transform.position = pos;
    }

}
