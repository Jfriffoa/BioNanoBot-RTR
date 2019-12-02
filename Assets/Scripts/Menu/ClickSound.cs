﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]

public class ClickSound : MonoBehaviour {

	public AudioClip Sound;

	private Button StartButton { get { return  GetComponent<Button> (); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } }


	void Start () {
		gameObject.AddComponent<AudioSource> ();
		source.clip = Sound;
		source.playOnAwake = false;

		//StartButton.onClick.AddListener(() => PlaySound (();
	}
	

	void PlaySound() {
		source.PlayOneShot (Sound);
	}
}
