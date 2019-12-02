using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour {
    [SerializeField]
    float m_frecuency = 1.0f;

    [SerializeField]
    float m_magnitude = 0.05f;

    [SerializeField]
    Vector3 m_direction = Vector3.right;

    float m_generatedTime;

    void Start() {
        m_generatedTime = Time.time;
    }

    void Update() {
        if (GameState.Paused()) {
            m_generatedTime += Time.deltaTime;
            return;
        }

        UpdateMove();
    }

    void UpdateMove() {
        float deltaTime = Time.time - m_generatedTime;
        transform.position += m_direction * Mathf.Sin(deltaTime * m_frecuency) * m_magnitude;
    }
}
