//#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerPhaseTwo : MonoBehaviour {

    public GameObject VictoryScreen;

    [SerializeField]
    float m_movementSpeed = 100;

    [SerializeField]
    float m_shootDelay = .5f;
    float m_lastTimeShooted;

    int m_lifes;
    public int Lifes { get { return m_lifes; } }

    [SerializeField]
    GameObject m_bullet;

    [SerializeField]
    Transform m_bulletSpawn;

    [SerializeField]
    Transform bulletsList;

    bool m_dying;
    bool m_damaged;

    [HideInInspector] public bool CanShoot { get; set; }

    PlayerStatePhaseTwo m_state;
    Collider2D m_collider;
    ParticleSystem m_particle;

    // Use this for initialization
    void Start () {
        m_dying = false;
        m_damaged = false;
        m_lastTimeShooted = Time.time;
        m_state = GameState.Instance.PlayerP2;
        m_lifes = GameState.Instance.ConfigP2.StartingLifes;
        m_collider = GetComponent<Collider2D>();
        m_particle = GetComponentInChildren<ParticleSystem>();

        CanShoot = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameState.Paused()) {
            StopAllCoroutines();

            if (GameInput.Instance.Next)
                GameInput.Instance.ChangePause();
            else if (GameInput.Instance.Back)
                ScenesManager.Restart();

            return;
        }


        if (m_dying)
            return;

        UpdateMove();

        if (CanShoot)
            UpdateShooting();

        UpdateRotation();
	}

    void UpdateMove() {
        float moveHorizontal = GameInput.Instance.MoveHorizontal;
        float moveVertical = GameInput.Instance.MoveVertical;

        float dx = moveHorizontal * m_movementSpeed * Time.deltaTime;
        float dy = moveVertical * m_movementSpeed * Time.deltaTime;

        Vector3 newPos = new Vector2(dx, dy);

        Vector2 minScreen = Camera.main.WorldToScreenPoint(m_collider.bounds.min + newPos);
        Vector2 maxScreen = Camera.main.WorldToScreenPoint(m_collider.bounds.max + newPos);

        if (minScreen.x < 0 || maxScreen.x > Screen.width)
            dx = 0;
        if (minScreen.y < 0 || maxScreen.y > Screen.height)
            dy = 0;

        transform.position = new Vector2(transform.position.x + dx, transform.position.y + dy);
    }

    public void Die() {
        Debug.Log("Lifes: " + m_lifes);
        if (m_damaged)
            return;

        m_lifes--;

        if (m_lifes > 0) {
            m_damaged = true;
            StartCoroutine(DamagedAnimation());
        } else {
            m_dying = true;
            //Disable the renderer and the Collider
            GetComponent<SpriteRenderer>().enabled = false;
            m_collider.enabled = false;

            m_particle.Play();
            StartCoroutine(KillAnimation());
        }
    }

    IEnumerator DamagedAnimation() {
        Animator an = GetComponent<Animator>();
        an.SetBool("Damaged", true);
        an.Play("Damaged");
        yield return new WaitForSeconds(3f);
        an.SetBool("Damaged", false);
        m_damaged = false;
    }

    IEnumerator KillAnimation() {
        while (m_particle.isPlaying) {
            yield return null;
        }
        ObjectPool.Kill(gameObject);
        ScenesManager.Restart();
    }

    void UpdateShooting() {
        if (GameInput.Instance.Shoot && Time.time - m_lastTimeShooted > m_shootDelay) {
            //Debug.Log("SHOOT " + Time.time + " - " + m_shootDelay + " - " + (m_lastTimeShooted));

            m_lastTimeShooted = Time.time;
            ObjectPool.Instantiate(m_bullet, m_bulletSpawn.position, m_bulletSpawn.rotation, bulletsList);
            /*GameObject go = SingleObjectPool.GetObject();
            go.transform.position = m_bulletSpawn.position;
            go.transform.rotation = m_bulletSpawn.rotation;
            go.SetActive(true);*/
            //Instantiate<GameObject>(m_bullet, m_bulletSpawn.position, m_bulletSpawn.rotation, bulletsList);
        }
    }

    void UpdateRotation() {
        transform.rotation = Quaternion.identity;
    }

    public void GiveScore(int score) {
        m_state.GiveScore(score);
    }

    public void Win() {
        m_dying = true;
        StartCoroutine(WinAnimation());
    }

    IEnumerator WinAnimation() {
        Vector2 min;

        GameManager.StopAll();
        VictoryScreen.SetActive(true);

        do {
            yield return null;
            float dy = m_movementSpeed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, transform.position.y + dy);
            min = Camera.main.WorldToScreenPoint(m_collider.bounds.min);
        } while (min.y < Screen.height);

        ScenesManager.nextScene();
        Debug.Log("WIN");

        //yield return null;
    }
}
