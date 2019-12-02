using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>{

    [SerializeField]
    GameObject m_boss;

    [SerializeField]
    GameObject m_bossUI;

    [SerializeField]
    GameObject m_uiCanvas;

    [SerializeField]
    GameObject m_pauseCanvas;

    [SerializeField]
    GameObject m_pauseBg;

    float m_timeToFindBoss;
    float m_startedTime;
    bool m_bossSpawned;

    static GameObject[] m_spawners;

    [HideInInspector]
    public bool tutorial;

    void Start () {
        m_timeToFindBoss = GameState.Instance.ConfigP2.TimeToFindBoss;
        m_startedTime = Time.time;
        m_bossSpawned = false;
        m_spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }
	
	void Update () {
        if (GameState.Paused() || GameState.Tutorial()) {
            m_startedTime += Time.deltaTime;
            return;
        }

        if (Time.time - m_startedTime > m_timeToFindBoss && !m_bossSpawned) {
            Debug.Log("BOSS FINDED");
            SpawnBoss();
            m_bossSpawned = true;
        }
	}

    void SpawnBoss() {
        m_boss = ObjectPool.Instantiate(m_boss, m_boss.transform.position, m_boss.transform.rotation);
    }

    void ActiveBUI() {
        if (m_bossUI == null)
            m_bossUI = GameObject.FindGameObjectWithTag("BossUI");
        m_bossUI.SetActive(true);
        m_bossUI.GetComponentInChildren<BossBar>().boss = m_boss;
    }

    public void LetsStart() {
        Start();
    }

    public static void StopAll() {
        GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject bg in bgs) {
            bg.GetComponent<BgScrolling>().Stop();
        }

        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners) {
            spawner.GetComponent<Spawner>().enabled = false;
        }
    }

    public static void DeactiveSpawners() {
        foreach (GameObject spawner in m_spawners) {
            spawner.GetComponent<Spawner>().enabled = false;
        }
    }

    public static void ActiveSpawners() {
        foreach (GameObject spawner in m_spawners) {
            spawner.GetComponent<Spawner>().enabled = true;
        }
    }

    public static void ActiveBossUI() {
        Instance.ActiveBUI();
    }

    public void Pause() {
        m_uiCanvas.SetActive(false);
        m_pauseCanvas.SetActive(true);
        m_pauseBg.SetActive(true);
    }

    public void Despause() {
        m_uiCanvas.SetActive(true);
        m_pauseCanvas.SetActive(false);
        m_pauseBg.SetActive(false);
    }
}
