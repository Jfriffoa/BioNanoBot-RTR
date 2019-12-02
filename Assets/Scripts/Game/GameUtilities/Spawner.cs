using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject toSpawn;

    public float minSpawnRate;
    public float maxSpawnRate;

    [SerializeField]
    float minX;

    [SerializeField]
    float maxX;

    [SerializeField]
    float minY;

    [SerializeField]
    float maxY;

    [SerializeField]
    bool spawnOutOfCamera = false;

    int enemiesSpawned;
    public int EnemiesSpawned { get { return enemiesSpawned; } }

    float lastGeneratedTime;
    Camera m_camera;

	void Start () {
        this.lastGeneratedTime = Time.time;
        m_camera = Camera.main;
        enemiesSpawned = 0;
	}
	
	void Update () {
        if (GameState.Paused()) {
            lastGeneratedTime += Time.deltaTime;
            return;
        }

        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        if (Time.time - lastGeneratedTime > spawnRate) {
            Spawn();
            lastGeneratedTime = Time.time;
        }
	}

    public void Spawn() {
        float y = Random.Range(minY, maxY);
        float x = Random.Range(minX, maxX);

        if (spawnOutOfCamera) {
            Vector2 whereToSpawn = m_camera.WorldToScreenPoint(new Vector2(x, y));

            if (!m_camera.pixelRect.Contains(whereToSpawn) && !GetComponent<Collider>().isTrigger)
                ObjectPool.Instantiate(toSpawn, new Vector2(x, y), toSpawn.transform.rotation, transform);

        } else {
            GameObject go = ObjectPool.Instantiate(toSpawn, new Vector2(x, y), toSpawn.transform.rotation, transform);

            //Specific lines to make CircularMovement works correctly
            CircularMovement cm = go.GetComponent<CircularMovement>();
            if (cm != null) {
                cm.SetPivot(new Vector2(x, y));
                //Debug.Log("OX: " + x + " OY: " + y + " X: " + cm.Pivot.x + " Y: " + cm.Pivot.y);
            }
        }

        enemiesSpawned++;
    }
}
