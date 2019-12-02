using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redSea : MonoBehaviour {
	[SerializeField] Slider slider;

	SpriteRenderer e_redSeaSpriteRenderer;
	float startTime;
	float grad;
	float MaxGrad;

	// Use this for initialization
	void Start () {
		MaxGrad = grad;

		e_redSeaSpriteRenderer = GetComponent<SpriteRenderer> ();
		grad = (e_redSeaSpriteRenderer.color.a * 0.1f);
		startTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (slider.value >= (slider.maxValue / 2) && grad >= 0f) {
			reduceAlpha ();
		} else if (slider.value <= (slider.maxValue / 2) && grad <= MaxGrad ) {
			increaseAlpha ();
		}


	}

	void reduceAlpha (){
		float timeStamp = Time.time - startTime;

		e_redSeaSpriteRenderer.color = new Color (
			e_redSeaSpriteRenderer.color.r, 
			e_redSeaSpriteRenderer.color.g, 
			e_redSeaSpriteRenderer.color.b, e_redSeaSpriteRenderer.color.a - grad);

		if (timeStamp > 1) {
			grad = grad - 0.01f;
			startTime = Time.time;
		}
	}

	void increaseAlpha() {
		float timeStamp = Time.time - startTime;

		e_redSeaSpriteRenderer.color = new Color (
			e_redSeaSpriteRenderer.color.r, 
			e_redSeaSpriteRenderer.color.g, 
			e_redSeaSpriteRenderer.color.b, e_redSeaSpriteRenderer.color.a + grad);

		if (timeStamp > 1) {
			grad = grad + 0.01f;
			startTime = Time.time;
		}
	}
}
