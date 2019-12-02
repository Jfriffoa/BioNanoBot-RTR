using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviourSingleton<ScenesManager> {

    public string[] scenes;

    private int index = 1;

    public ConfigPhaseOne cfo;
    public ConfigPhaseTwo cft;

    public static void LoadByIndex(int sceneIndex) {
        if (sceneIndex < Instance.scenes.Length) {
            Instance.index = sceneIndex + 1;
            SceneManager.LoadScene(Instance.scenes[sceneIndex]);
        } else {
            Debug.Log("Indice fuera de los parametros");
            Application.Quit();
        }
    }

    public static void nextScene() {
        if (Instance.index >= Instance.scenes.Length) {
            Restart();
            return;
        }

        if (Instance.index == 5) {
            GameState.CreateGame(Instance.cfo, Instance.cft, 1);
        } else if (Instance.index == 3) {
            GameState.CreateGame(Instance.cfo, Instance.cft, 2);
        } else
            SceneManager.LoadScene(Instance.scenes[Instance.index]);

        Instance.index++;
    }

    public static void Restart() {
        Instance.index = 0;
        MainMenuController.check = true;
        nextScene();
    }
}
