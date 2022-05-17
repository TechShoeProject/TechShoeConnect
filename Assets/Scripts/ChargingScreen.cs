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
        if (PlayerPrefs.GetInt("Theme", -50) == -50) PlayerPrefs.SetInt("Theme", 1);
        PlayerPrefs.SetFloat("Theme1Background_r", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Background_g", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Background_b", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Text_r", 0f);
        PlayerPrefs.SetFloat("Theme1Text_g", 0f);
        PlayerPrefs.SetFloat("Theme1Text_b", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_r", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_g", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_b", 0f);
        PlayerPrefs.SetFloat("Theme1LogoO_r", 1f);
        PlayerPrefs.SetFloat("Theme1LogoO_g", 1f);
        PlayerPrefs.SetFloat("Theme1LogoO_b", 1f);
        PlayerPrefs.SetFloat("Theme1Checkmark_r", 0.55f);
        PlayerPrefs.SetFloat("Theme1Checkmark_g", 0.55f);
        PlayerPrefs.SetFloat("Theme1Checkmark_b", 0.55f);
        PlayerPrefs.SetFloat("Theme1RedButton_r", 0.5f);
        PlayerPrefs.SetFloat("Theme1RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme1RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme1Button_r", 0.4f);
        PlayerPrefs.SetFloat("Theme1Button_g", 0.4f);
        PlayerPrefs.SetFloat("Theme1Button_b", 0.4f);
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
