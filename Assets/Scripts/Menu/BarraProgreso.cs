using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraProgreso : MonoBehaviour {

	public Transform BarraEspera;
	public Transform TextProgreso;
	public Transform TextCargando;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

    public string nextSceneName;

	// Update is called once per frame
	void Update () {
		if (currentAmount < 100) {
			currentAmount += speed * Time.deltaTime;
			TextProgreso.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
			TextCargando.gameObject.SetActive (true);

		} else {
			TextCargando.gameObject.SetActive (false);
			TextProgreso.GetComponent<Text> ().text = "Listo!";
            ScenesManager.nextScene();
		}
		BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
