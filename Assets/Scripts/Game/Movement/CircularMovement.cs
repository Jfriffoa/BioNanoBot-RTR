using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour {
    [SerializeField]
    float m_speed = 1.0f;

    [SerializeField]
    float m_radius = 1.0f;

    [SerializeField]
    bool m_clockwise;

    [SerializeField]
    bool m_moveCircle;

    [SerializeField]
    float m_movementSpeed;

    [SerializeField]
    Vector2 m_direction = Vector2.right;


    float angle = 0f;
    Vector2 m_pivot;

    public Vector2 Pivot { get { return m_pivot; } }
    public void SetPivot(Vector2 value) {
        m_pivot = value;
    }

    void Start() {
        m_pivot = transform.position;
    }

    void Update() {
        if (GameState.Paused())
            return;

        if (m_moveCircle)
            UpdateMove();

        UpdateCircle();
    }

    void UpdateMove() {
        m_pivot += m_direction * m_movementSpeed * Time.deltaTime;
    }

    void UpdateCircle() {
        if (!m_clockwise)
            angle += m_speed * Time.deltaTime;
        else
            angle -= m_speed * Time.deltaTime;


        float x = Mathf.Cos(angle) * m_radius + m_pivot.x;
        float y = Mathf.Sin(angle) * m_radius + m_pivot.y;
        transform.position = new Vector2(x, y);
    }
}
