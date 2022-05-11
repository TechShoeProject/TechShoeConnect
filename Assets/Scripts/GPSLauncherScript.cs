using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSLauncherScript : MonoBehaviour
{
    private void Start()
    {
        //if (PlayerPrefs.GetInt("GPSRequestTime") == 0)
            PlayerPrefs.SetInt("LaunchGPS", 1);
    }
}
