using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour {

    public GameObject boss;

    RectTransform m_transform;

    float originalLifes;
    float originalHeight;

    void Start() {
        m_transform = GetComponent<RectTransform>();
        originalLifes = GameState.Instance.ConfigP2.BossLife;
        originalHeight = m_transform.sizeDelta.y;
    }

    void Update() {
        float lifes = boss.GetComponent<Boss>().Life;
        float y = (lifes / originalLifes * 1.0f) * originalHeight;
        m_transform.sizeDelta = new Vector2(m_transform.sizeDelta.x, y);
    }
}