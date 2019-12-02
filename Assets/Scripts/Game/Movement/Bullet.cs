using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float m_speed = 5f;

    Rigidbody2D m_rigidbody;
	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Paused()) {
            m_rigidbody.velocity = Vector2.zero;
            return;
        }
        if (GameState.Paused())
            return;

        UpdatePosition();
	}

    void UpdatePosition() {
        m_rigidbody.velocity = Vector2.up * m_speed;
    }

}
