using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] GameObject m_bullet;
    [SerializeField] int m_catridgeSize = 8;
    [SerializeField] int m_shootsPerWave = 3;
    [SerializeField] float m_shootsDelay = .5f;
    [SerializeField] float timeToClose = 10f;
    [SerializeField] float timeToOpen = 15f;

    float m_life;
    public float Life { get { return m_life; } set { m_life = value; } }

    Animator m_anim;
    bool objectsRunning = true;

    bool m_active = false;
    int enteringHash = Animator.StringToHash("Base Layer.Enter");
    ParticleSystem m_particle;
    GameObject m_pearl;
    PlayerPhaseTwo m_player;

    //Maximum Damage taken
    float m_maxDmg;
    bool m_open;

    float m_lastLifeCount;
    float m_lastTimeChanged;

    void Start () {
        m_life = GameState.Instance.ConfigP2.BossLife;
        m_maxDmg = m_life * 0.34f;
        m_anim = GetComponent<Animator>();
        m_anim.Play("Enter");
        m_particle = GetComponentInChildren<ParticleSystem>();
        m_pearl = GetComponentInChildren<Pearl>().gameObject;
        m_pearl.SetActive(false);
        m_open = false;
        m_lastLifeCount = m_life;
        m_lastTimeChanged = Time.time;
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPhaseTwo>();
    }

    void Update() {
        if (GameState.Paused() || GameState.Tutorial()) {
            StopAllCoroutines();
            m_anim.speed = 0;
            m_lastTimeChanged += Time.deltaTime;
            return;
        }

        if (m_anim.speed == 0)
            m_anim.speed = 1;

        //Get the animator to check if the Boss is already in the screen or it is the entering animation.
        AnimatorStateInfo stateInfo = m_anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.fullPathHash != enteringHash) {
            m_active = true;
        }

        if (!m_active)
            return;

        if (objectsRunning) {
            GameManager.StopAll();
            setSpawnerTimers();
            GameManager.ActiveBossUI();
            objectsRunning = false;
        }

        CheckUpdate();
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if (m_open)
            return;

        if (collision.gameObject.name.Contains("PlayerBullet")) {
            ObjectPool.Kill(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerPhaseTwo>().Die();
        }
    }

    public void TakeDamage() {
        if (!m_active)
            return;

        m_life -= 100;
        Debug.Log("BOSS LIFE: " + m_life);
        /*StopCoroutine(DamagedAnimation());
        StartCoroutine(DamagedAnimation());*/

        if (m_lastLifeCount - m_life > m_maxDmg)
            UpdatePhase();

        if (m_life <= 0) {
            Debug.Log("THE BOSS HAS BEEN DEFEATED");
            Kill();
            m_player.Win();
        }
    }

    /*IEnumerator DamagedAnimation() {
        for (float f = 0; f <= 1; f += 0.2f) {
            m_renderer.color = new Color(215f / 255 * f, 63f / 255 * f, 63f / 255 * f);
            Debug.Log("CHANGE: " + m_renderer.color.r + ", " + m_renderer.color.g + ", " + m_renderer.color.b);
            yield return new WaitForSeconds(0.1f);
        }

        for (float f = 1; f >= 0; f -= 0.2f) {
            m_renderer.color = new Color(215f / 255 * f, 63f / 255 * f, 63f / 255 * f);
            Debug.Log("CHANGE BACK: " + m_renderer.color.r + ", " + m_renderer.color.g + ", " + m_renderer.color.b);
            yield return new WaitForSeconds(0.1f);
        }
    }*/

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

        m_particle.Play();
        StartCoroutine(KillAnimation());
    }

    IEnumerator KillAnimation() {
        while (m_particle.isPlaying) {
            yield return null;
        }
        ObjectPool.Kill(gameObject);
    }

    void CheckUpdate() {
        float timeToChange;
        if (m_open)
            timeToChange = timeToClose;
        else
            timeToChange = timeToOpen;

        if (Time.time - m_lastTimeChanged > timeToChange) {
            UpdatePhase();
        }
    }

    void UpdatePhase() {
        AnimatorStateInfo asi = m_anim.GetCurrentAnimatorStateInfo(0);
        float animationTime = asi.normalizedTime;
        Debug.Log("ANIMATION TIME: " + animationTime);
        if (m_open) {
            m_anim.Play("Closed", 0, animationTime);
            m_pearl.SetActive(false);
            GameManager.ActiveSpawners();
        } else {
            GameManager.DeactiveSpawners();
            m_anim.Play("Open", 0, animationTime);
            m_pearl.SetActive(true);
            m_lastLifeCount = m_life;
            StartCoroutine(ShootingCatridges());
        }
        m_open = !m_open;
        m_anim.SetBool("Open", m_open);
        m_lastTimeChanged = Time.time;
    }

    void setSpawnerTimers() {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        
        foreach(GameObject spawner in spawners) {
            Spawner script = spawner.GetComponent<Spawner>();
            script.minSpawnRate = 1;
            script.maxSpawnRate = 4;
        }
    }

    IEnumerator ShootingCatridges() {
        for (int i = 0; i < m_shootsPerWave; i++) {
            Shoot();
            yield return new WaitForSeconds(m_shootsDelay);
        }
    }

    void Shoot() {
        float rng = Random.Range(0, 180 / m_catridgeSize);
        for (int i = 0; i < m_catridgeSize; i++) {
            float args = (Mathf.PI * i / m_catridgeSize) + Mathf.PI + rng * Mathf.Deg2Rad;
            float x = Mathf.Cos(args);
            float y = Mathf.Sin(args);
            m_bullet.GetComponent<StraightMovement>().m_direction = new Vector2(x, y);
            ObjectPool.Instantiate(m_bullet, transform.position, Quaternion.Euler(0,0,Mathf.Rad2Deg * args + 90));
        }
    }
}