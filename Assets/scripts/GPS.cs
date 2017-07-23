using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{

	public static GPS Instance{ set; get; }
	public float lattitude;
	public float longitude;

	private void Start ()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);
		StartCoroutine (StartLocationService());
	}
	private IEnumerator StartLocationService()
	{
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log ("Location is Not enabled by user ");
			yield break;
		}

		Input.location.Start (.1f,.1f);
		int maxwait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxwait > 0)
		{
			yield return new WaitForSeconds (1);
			maxwait--;
		}

		if (maxwait <= 0)
		{
			Debug.Log ("Timed Out");
			yield break;
		}

		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log ("Unable to determine location");
			yield break;
		}

		lattitude = Input.location.lastData.latitude;
		longitude = Input.location.lastData.longitude;
		yield break;
	}
}
