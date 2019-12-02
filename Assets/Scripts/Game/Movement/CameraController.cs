using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] GameObject e_toFollowObject;

	Camera e_camera;


	// Use this for initialization
	void Start () {
		e_camera = GetComponent<Camera> ();
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		
		e_camera.transform.position = new Vector3 ( e_toFollowObject.transform.position.x, 
													e_toFollowObject.transform.position.y , 
													e_camera.transform.position.z);;
		
	}

}
