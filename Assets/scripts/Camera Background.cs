using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBackground : MonoBehaviour {

	private WebCamTexture backCam;
	public RawImage image;

	// Use this for initialization
	void Start ()
	{
		image = GetComponent <RawImage> ();
		backCam = new WebCamTexture (Screen.width, Screen.height);
		image.texture = backCam;
		backCam.Play ();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
