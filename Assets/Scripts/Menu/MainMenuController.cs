using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public GameObject[] buttons;

    int optionIndex = 0;

    public GameObject helpPanel;
    public GameObject audioMenu;
    public int creditsIndex;

    public SoundManager sound;

    public static bool check = true;

    void Start() {
        //UpdateSize();
    }

    // Update is called once per frame
    void Update () {
        if (!check)
            return;

        if (sound == null)
            sound = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();

		//if (GameInput.Instance.Up || GameInput.Instance.Down) {
  //          sound.Onhover();

  //          if (GameInput.Instance.Down) {
  //              optionIndex--;
  //              if (optionIndex < 0)
  //                  optionIndex = buttons.Length - 1;
  //          } else {
  //              optionIndex = (optionIndex + 1) % buttons.Length;
  //          }

  //          UpdateSize();
  //      }

        //if (GameInput.Instance.Back) {
        //    sound.Onclick();
        //    Exit();
        //}

        if (GameInput.Instance.Next || GameInput.Instance.Pause) {
            sound.Onclick();
            switch (optionIndex) {
                case 0: Play(); break;
                //case 1: Exit(); break;
                //case 2: Help();    break;
                //case 3: Credits(); break;
                //case 4: Exit();    break;
            }
        }

	}

    void Play() {
        ScenesManager.nextScene();
    }

    void Options() {
        check = false;
        helpPanel.SetActive(false);
        audioMenu.SetActive(true);
    }

    void Help() {
        check = false;
        helpPanel.SetActive(true);
        audioMenu.SetActive(false);
    }

    void Credits() {
        ScenesManager.LoadByIndex(creditsIndex);
    }

    void Exit() {
        #if UNITY_EDITOR
		        UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    void UpdateSize() {
        for (int i = 0; i < buttons.Length; i++) {
            if (i == optionIndex)
                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            else
                buttons[i].GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
