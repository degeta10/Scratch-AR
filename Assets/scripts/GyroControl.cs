using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
	private bool gyroEnabled;
	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rotfix;

	[SerializeField]
	private Transform world;
	private float StartY;

	void Start()
	{
		gyroEnabled = SystemInfo.supportsGyroscope;
		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;
		transform.parent= cameraContainer.transform;

		if (gyroEnabled) 
		{
			gyro = Input.gyro;
			gyro.enabled = true;
			cameraContainer.transform.rotation = Quaternion.Euler (90f, 180f, 0f);
			rotfix = new Quaternion (0, 0, 1, 0);
		}
	}
	void Update()
	{
		if (gyroEnabled && StartY==0)
		{
			ResetGyroRotation ();

		}
		transform.localRotation = gyro.attitude * rotfix;		
	}

	void ResetGyroRotation ()
	{
		StartY = transform.eulerAngles.y;
		world.rotation = Quaternion.Euler (0f, StartY, 0f);
	}
}



