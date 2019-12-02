using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    int m_score = 100;

    [SerializeField]
    GameObject m_particle;

    [SerializeField]
    Color m_particleColor = Color.HSVToRGB(69f/359f, 215f/255f, 130f/255f);

    void Start() {
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("PlayerBullet")) {
            //Debug.Log("PLAYER HITS ME");
            //Destroy(gameObject);
            ObjectPool.Kill(collision.gameObject);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerPhaseTwo>().GiveScore(m_score);
            Kill();
        }
    }

    void Kill() {
        SineMovement sm = GetComponent<SineMovement>();
        if (sm != null)
            sm.enabled = false;

        StraightMovement strm = GetComponent<StraightMovement>();
        if (strm != null)
            strm.enabled = false;

        FollowObject fo = GetComponent<FollowObject>();
        if (fo != null)
            fo.enabled = false;

        CircularMovement cm = GetComponent<CircularMovement>();
        if (cm != null)
            cm.enabled = false;

        //Disable the renderer and the Collider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (m_particle != null) {
            m_particle = ObjectPool.Instantiate(m_particle, transform.position, transform.rotation);
            StartCoroutine(KillAnimation());
        } else
            ObjectPool.Kill(gameObject);

    }

    IEnumerator KillAnimation() {
        ParticleSystem system = m_particle.GetComponent<ParticleSystem>();
        system.startColor = m_particleColor;
        system.Play();
        while (system.isPlaying) {
            yield return null;
        }
        ObjectPool.Kill(gameObject);
        ObjectPool.Kill(m_particle);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (collision.gameObject.GetComponent<PlayerPhaseOne>() != null)
                collision.gameObject.GetComponent<PlayerPhaseOne>().sustractLife();
            else
                collision.gameObject.GetComponent<PlayerPhaseTwo>().Die();
            Kill();
        }
    }
}
