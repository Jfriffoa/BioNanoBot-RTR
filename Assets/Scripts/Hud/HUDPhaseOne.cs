using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPhaseOne : MonoBehaviour {

	PlayerStatePhaseOne playerState;

	[SerializeField] Slider slider;

	[SerializeField] Text time;
	float fTime;
	float StartTime;

	[SerializeField] Text porcentaje;
 
	[SerializeField] Spawner enemySpawner;
	int cantidadDeEnemigosGenerados;

	float cleaningBarMaxValue;
	float cleaningBarPlayerScore;


    public GameObject VictoryScreen;
    public GameObject GameOverScreen;

    bool updateTime = true;

	// Use this for initialization
	void Start () {
		//GameState.Instance.Pause();


		playerState = GameState.Instance.PlayerP1;

		cleaningBarMaxValue = slider.maxValue;
		cleaningBarPlayerScore = slider.value;

		cantidadDeEnemigosGenerados = enemySpawner.EnemiesSpawned;

		fTime = 60f;
		StartTime = Time.time;

	}


	
	// Update is called once per frame
	void Update () {

		UpdateEnemigosGenerados ();
		UpdateCleaningBarMaxValue ();
		UpdateCleaningBarPlayerScore ();
		UpdatePorcentaje ();
		UpdateTime ();

        if (fTime >= 0 && updateTime)
		    UpdateTimeOnCanvas ();

        if (fTime == 0) {
            CheckWin();
        }
	}

	void UpdateEnemigosGenerados(){
		cantidadDeEnemigosGenerados = enemySpawner.EnemiesSpawned;
	}

	void UpdateCleaningBarMaxValue(){
		slider.maxValue = cleaningBarMaxValue + (cantidadDeEnemigosGenerados * 5);

	}

	void UpdateCleaningBarPlayerScore(){
		slider.value = playerState.getCleaningScore;

	}

	public float getActualScore(){
		return this.slider.value;
	}

	void UpdatePorcentaje(){
		porcentaje.text = playerState.getPorcentajeR.ToString () + "%";
	}

	void UpdateTime(){
		float timeStamp = Time.time - StartTime;
		if (timeStamp >= 1.0f) {
			fTime--;
			StartTime = Time.time;
		}

	}

	void UpdateTimeOnCanvas(){
		time.text = "Time: " + fTime.ToString();
	}

    void CheckWin() {
        Debug.Log(slider.value + ", " + slider.maxValue);
        if (slider.value >= slider.maxValue / 2) {
            Debug.Log("FUCK YEAH");
            Win();
        } else {
            Lose();
        }
    }

    IEnumerator Winning() {
        GameState.Instance.Pause();
        VictoryScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        ScenesManager.nextScene();
    }

    IEnumerator Losing() {
        GameState.Instance.Pause();
        GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        ScenesManager.Restart();
    }

    public void Lose() {
        updateTime = false;
        StartCoroutine(Losing());
    }

    public void Win() {
        updateTime = false;
        StartCoroutine(Winning());
    }

    /*/188	
	void moveCientific(){
		float yActual = cientific.gameObject.transform.position.y;
		while (yActual < (-188) ) {
			yActual++;
			cientific.gameObject.transform.position = new Vector2 ( cientific.gameObject.transform.position.x, yActual );

		}

	}*/

}
