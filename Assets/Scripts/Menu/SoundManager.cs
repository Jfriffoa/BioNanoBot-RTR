using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviourSingleton<SoundManager> {
	public AudioSource source;
	public AudioClip hover;
	public AudioClip click;

    void Start() {
        if (source == null)
            source = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    public void Onhover(){
		source.PlayOneShot (hover);
	}

	public void Onclick (){
		source.PlayOneShot (click);
	}

}
