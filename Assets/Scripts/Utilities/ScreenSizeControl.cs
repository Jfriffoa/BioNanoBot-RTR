using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeControl : MonoBehaviour {

    public int widthRatio;
    public int heightRatio;

    void Awake() {
        Resolution res = Screen.currentResolution;
        int actualWidth = res.width;
        int actualHeight = res.height;

        float wRate = actualWidth / (widthRatio * 1.0f);
        float hRate = actualHeight / (heightRatio * 1.0f);

        int width = widthRatio * (int)Mathf.Min(wRate, hRate);
        int height = heightRatio * (int)Mathf.Min(hRate, wRate);

        Screen.SetResolution(width, height, false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
