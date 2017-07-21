using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerlessAR : MonoBehaviour {
	//Gyro
	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rot;

	//Cam
	private WebCamTexture Cam;
	public RawImage background;
	public AspectRatioFitter fit;

	private bool arReady = false;

	private void Start()
	{
		//Check if Both supports on i.e location and Gyroscope
		if (!SystemInfo.supportsGyroscope)
		{
			Debug.Log ("NO Gyroscope Found");
			return;
		}

		//BackCam
		for (int i = 0; i < WebCamTexture.devices.Length; i++)
		{

			if (!WebCamTexture.devices[i].isFrontFacing)
			{
				Cam = new WebCamTexture (WebCamTexture.devices [i].name, Screen.width, Screen.height);
				break;
			}
		}
		if (Cam == null)
		{
			Debug.Log ("This device has no Back camera");
			return;
		}

		//Both supports enabled
		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;
		transform.SetParent (cameraContainer.transform);

		gyro = Input.gyro;
		gyro.enabled = true;
		cameraContainer.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
		rot = new Quaternion (0, 0, 1, 0);

		Cam.Play ();
		background.texture = Cam;
		arReady = true;
	}

	private void Update()
	{
		if (arReady)
		{
			//Update cAmera
			float ratio = (float)Cam.width / (float)Cam.height;
			fit.aspectRatio = ratio;

			float scaleY= Cam.videoVerticallyMirrored?-1f:1f;
			background.rectTransform.localScale = new Vector3 (1f, scaleY, 1f);

			int orient = Cam.videoRotationAngle;
			background.rectTransform.localEulerAngles = new Vector3 (0, 0, orient);

			//Update Gyro
			transform.localRotation = gyro.attitude * rot;
		}

	}

}
