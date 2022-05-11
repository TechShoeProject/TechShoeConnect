using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChargingScreen : MonoBehaviour
{
    [SerializeField] Slider charging;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        PlayerPrefs.SetInt("LaunchGPS", 0);
        PlayerPrefs.SetInt("GPSRequestTime", 0);
        PlayerPrefs.SetString("Maneuver1", string.Empty);
        PlayerPrefs.SetString("Maneuver2", string.Empty);
        PlayerPrefs.SetString("Maneuver3", string.Empty);
        PlayerPrefs.SetString("Maneuver4", string.Empty);
        PlayerPrefs.SetFloat("Distance1", float.NaN);
        PlayerPrefs.SetFloat("Distance2", float.NaN);
        PlayerPrefs.SetFloat("Distance3", float.NaN);
        PlayerPrefs.SetFloat("Distance4", float.NaN);
        PlayerPrefs.SetInt("Duration1", 0);
        PlayerPrefs.SetInt("Duration2", 0);
        PlayerPrefs.SetInt("Duration3", 0);
        PlayerPrefs.SetInt("Duration4", 0);
        PlayerPrefs.SetString("NextAdressesNames1", string.Empty);
        PlayerPrefs.SetString("NextAdressesNames2", string.Empty);
        PlayerPrefs.SetString("NextAdressesNames3", string.Empty);
        PlayerPrefs.SetString("NextAdressesNames4", string.Empty);
        PlayerPrefs.SetString("NextAdressesNames5", string.Empty);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(1);
        while (!loadScene.isDone)
        {
            yield return null;
            charging.value = loadScene.progress;
            text.text = "Chargement : " + (int)(loadScene.progress * 100) + " %";
        }
        charging.value = 1;
        text.text = "Chargement : 100 %";
        yield return null;
        loadScene.allowSceneActivation = true;
    }
}
