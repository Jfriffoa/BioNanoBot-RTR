using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : MonoBehaviour {
    [SerializeField]
    float m_speed = 1.0f;

    public Vector3 m_direction = Vector3.down;

    void Update() {
        if (GameState.Paused() || GameState.Tutorial()) {
            return;
        }

        UpdateMove();
    }
    void UpdateMove() {
        transform.position += m_direction * m_speed * Time.deltaTime;
    }
}
