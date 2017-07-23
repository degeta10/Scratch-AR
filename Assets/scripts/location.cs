using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class location : MonoBehaviour {

	public static location Instance{ set; get; }

	public float originalLatitude;
	public float originalLongitude;
	public float currentLongitude;
	public float currentLatitude;

	public bool setOriginalValues = true;

	IEnumerator GetCoordinates ()
	{
		while (true) {
			// check if user has location service enabled
			if (!Input.location.isEnabledByUser)
				yield break;

			// Start service before querying location
			Input.location.Start (.1f, .1f);

			// Wait until service initializes
			int maxWait = 20;
			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
				yield return new WaitForSeconds (1);
				maxWait--;
			}

			// Service didn't initialize in 20 seconds
			if (maxWait < 1) {
				print ("Timed out");
				yield break;
			}

			// Connection has failed
			if (Input.location.status == LocationServiceStatus.Failed) {
				print ("Unable to determine device location");
				yield break;
			} else {
				// Access granted and location value could be retrieved
				//print ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

				//if original value has not yet been set save coordinates of player on app start
				if (setOriginalValues) {
					originalLatitude = Input.location.lastData.latitude;
					originalLongitude = Input.location.lastData.longitude;
					setOriginalValues = false;
				}

				//overwrite current lat and lon everytime
				currentLatitude = Input.location.lastData.latitude;
				currentLongitude = Input.location.lastData.longitude;
			}
			Input.location.Stop();
		}
	}
	private void Start()
	{
		Instance = this;
		StartCoroutine ("GetCoordinates");
	}
}
