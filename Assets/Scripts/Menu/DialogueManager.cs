using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

	public GameObject[] texts;
	private int actualText = 0;

	public void NextText(){
		texts [actualText].SetActive (false);
		actualText++;
        if (actualText < texts.Length)
            texts[actualText].SetActive(true);
        else
            GameObject.FindGameObjectWithTag("Manager").GetComponent<ScreenManager>().nextScreen();
	}		
}
