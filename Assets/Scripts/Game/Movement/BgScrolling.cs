using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScrolling : MonoBehaviour {

    public float speed;

    Vector2 m_startPosition;
    BoxCollider2D m_boxCollider;

    float m_time;

    void Start() {
        m_startPosition = transform.position;
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_time = Time.time;
    }

    void Update () {
        if (GameState.Paused()) {
            return;
        }

        m_time += Time.deltaTime;

        float height = m_boxCollider.size.y;
        //Debug.Log("Height: " + tempWorldDimension.y);
        float newPosition = Mathf.Repeat(m_time * speed, height);

        transform.position = m_startPosition + Vector2.down * newPosition;
	}

    public void Stop() {
        //transform.position = new Vector2(transform.position.x, Mathf.Ceil(transform.position.y));
        enabled = false;
    }
}
