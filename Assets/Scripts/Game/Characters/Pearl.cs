using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour {

    Boss m_boss;

    void Start() {
        m_boss = GetComponentInParent<Boss>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("PlayerBullet")) {
            ObjectPool.Kill(collision.gameObject);
            m_boss.TakeDamage();
        }
    }
}
