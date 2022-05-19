using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSLauncherScript : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("LaunchGPS", 1);
    }
}
