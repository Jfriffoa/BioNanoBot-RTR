using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPhaseOne : MonoBehaviour {
	[SerializeField] Button button;
	[SerializeField] GameObject Cientist;
	[SerializeField] Image dialogueBackground;

	bool start = true;
	bool ended = false;

	public GameObject[] texts;
	private int actualText = 0;

	public void nextText(){
        /*
		if (start == true) {
			StartCoroutine (Dialogue());
			start = false;
		}

		texts [actualText].SetActive (false);
		actualText++;
		if (actualText < texts.Length) {
			texts [actualText].SetActive (true);
		} else if (texts [actualText] == null) {
			ended = true;
		}

		if (ended == true) {
			StartCoroutine (endDialogue ());
		}*/

	}

	IEnumerator endDialogue(){
		gameObject.SetActive (false);
		dialogueBackground.gameObject.SetActive (false);


		for(int i = 0; i < 10 ; i++){
			Debug.Log ("Pase por aqui");
			float yPos = Cientist.transform.position.y;
			yPos = yPos - (i*10);
			Cientist.transform.position = new Vector2 (
				Cientist.transform.position.x, yPos);
		}
		yield return new WaitForSeconds (3);
		//yield return new WaitForSecondsRealtime (3);

		//GameState.Instance.pauseGame(false);

	}

	IEnumerator Dialogue(){
		Cientist.gameObject.SetActive (true);
		Cientist.GetComponent<Animator> ().Play("MoveCientific");

		dialogueBackground.gameObject.SetActive (false);
		gameObject.SetActive (false);

		yield return new WaitForSecondsRealtime (2);

		dialogueBackground.gameObject.SetActive (true);
		gameObject.SetActive (true);

	}

	public bool getIsEnded(){
		return this.ended;
	}
}
