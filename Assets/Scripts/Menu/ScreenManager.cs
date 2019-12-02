using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    public GameObject[] screens;
    public DialogueManager[] dialogues;
    public string nextSceneName;

    private int index = 0;

	void Start () {
        screens[index].SetActive(true);
		for (int i = 1; i < screens.Length; i++) {
            screens[i].SetActive(false);
        }
	}

    void Update() {
        if (GameInput.Instance.Next)
            dialogues[index].NextText();

        if (GameInput.Instance.Pause)
            ScenesManager.nextScene();

        if (GameInput.Instance.Back)
            ScenesManager.Restart();
    }

    public void nextScreen() {
        screens[index].SetActive(false);
        index++;
        if (index < screens.Length)
            screens[index].SetActive(true);
        else
            ScenesManager.nextScene();
            
    }
	
}
