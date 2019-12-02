using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreen : MonoBehaviour {

    [SerializeField]
    float m_offset = 0f;
    [SerializeField]
    bool m_checkUp;
    [SerializeField]
    bool m_checkLeft;
    [SerializeField]
    bool m_checkRight;
    [SerializeField]
    bool m_checkDown;
    [SerializeField]
    bool m_destroy;


    Collider2D m_collider;

    void Start() {
        m_collider = GetComponent<Collider2D>();
    }

    void Update() {
        if (GameState.Paused()) {
            return;
        }

        Vector2 min = m_collider.bounds.min;
        Vector2 max = m_collider.bounds.max;

        Vector3 screenMinPos = Camera.main.WorldToScreenPoint(min);
        Vector3 screenMaxPos = Camera.main.WorldToScreenPoint(max);

        if (m_checkUp && screenMinPos.y > Screen.height + m_offset) {
            //Debug.Log("Destroy from the Upside");
            if (m_destroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }

        if (m_checkLeft && screenMaxPos.x < 0 - m_offset) {
            //Debug.Log("Destroy from the Leftside");
            if (m_destroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }

        if (m_checkRight && screenMinPos.x > Screen.width + m_offset) {
            //Debug.Log("Destroy from the Rightside");
            if (m_destroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }

        if (m_checkDown && screenMaxPos.y < 0 - m_offset) {
            //Debug.Log("Destroy from the Downside");
            if (m_destroy)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}
