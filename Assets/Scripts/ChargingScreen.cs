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
    GameObject multiScriptHolder;
    void Start()
    {
        PlayerPrefs.SetInt("Connected", 0);
        PlayerPrefs.SetInt("Dangerosite", 0);
        PlayerPrefs.SetInt("Son", 0);
        PlayerPrefs.SetInt("Distance", 0);
        PlayerPrefs.SetInt("BatterieStatus", -1);
        PlayerPrefs.SetInt("ChaussageStatus", -1);
        PlayerPrefs.SetInt("PasStatus", -1);
        PlayerPrefs.SetInt("Variation", 0);
        PlayerPrefs.SetInt("LastData4", -1);
        PlayerPrefs.SetInt("LastData3", -1);
        PlayerPrefs.SetInt("LastData2", -1);
        PlayerPrefs.SetInt("LastData1", -1);
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

        PlayerPrefs.SetInt("LastData4", -1);
        PlayerPrefs.SetInt("LastData3", -1);
        PlayerPrefs.SetInt("LastData2", -1);
        PlayerPrefs.SetInt("LastData1", -1);

        PlayerPrefs.SetInt("BatterieInfos", 0);
        PlayerPrefs.SetInt("PasInfos", 0);
        PlayerPrefs.SetInt("ConnectionInfos", 0);
        PlayerPrefs.SetInt("ChaussageInfos", 0);

        if (PlayerPrefs.GetInt("Theme", -50) == -50) PlayerPrefs.SetInt("Theme", 1);
        if (PlayerPrefs.GetInt("Police", -50) == -50) PlayerPrefs.SetInt("Police", 0);

        PlayerPrefs.SetFloat("Theme1Background_r", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Background_g", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Background_b", 0.2169811f);
        PlayerPrefs.SetFloat("Theme1Text_r", 0f);
        PlayerPrefs.SetFloat("Theme1Text_g", 0f);
        PlayerPrefs.SetFloat("Theme1Text_b", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_r", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_g", 0f);
        PlayerPrefs.SetFloat("Theme1Logo_b", 0f);
        PlayerPrefs.SetFloat("Theme1LogoO_r", 0.6f);
        PlayerPrefs.SetFloat("Theme1LogoO_g", 0.6f);
        PlayerPrefs.SetFloat("Theme1LogoO_b", 0.6f);
        PlayerPrefs.SetFloat("Theme1Checkmark_r", 0.55f);
        PlayerPrefs.SetFloat("Theme1Checkmark_g", 0.55f);
        PlayerPrefs.SetFloat("Theme1Checkmark_b", 0.55f);
        PlayerPrefs.SetFloat("Theme1RedButton_r", 0.5f);
        PlayerPrefs.SetFloat("Theme1RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme1RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme1Button_r", 0.4f);
        PlayerPrefs.SetFloat("Theme1Button_g", 0.4f);
        PlayerPrefs.SetFloat("Theme1Button_b", 0.4f);


        PlayerPrefs.SetFloat("Theme0Background_r", 1f);
        PlayerPrefs.SetFloat("Theme0Background_g", 1f);
        PlayerPrefs.SetFloat("Theme0Background_b", 1f);
        PlayerPrefs.SetFloat("Theme0Text_r", 0f);
        PlayerPrefs.SetFloat("Theme0Text_g", 0f);
        PlayerPrefs.SetFloat("Theme0Text_b", 0f);
        PlayerPrefs.SetFloat("Theme0Logo_r", 0f);
        PlayerPrefs.SetFloat("Theme0Logo_g", 0f);
        PlayerPrefs.SetFloat("Theme0Logo_b", 0f);
        PlayerPrefs.SetFloat("Theme0LogoO_r", 0.5f);
        PlayerPrefs.SetFloat("Theme0LogoO_g", 0.5f);
        PlayerPrefs.SetFloat("Theme0LogoO_b", 0.5f);
        PlayerPrefs.SetFloat("Theme0Checkmark_r", 0.9f);
        PlayerPrefs.SetFloat("Theme0Checkmark_g", 0.9f);
        PlayerPrefs.SetFloat("Theme0Checkmark_b", 0.9f);
        PlayerPrefs.SetFloat("Theme0RedButton_r", 1f);
        PlayerPrefs.SetFloat("Theme0RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme0RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme0Button_r", 0.9f);
        PlayerPrefs.SetFloat("Theme0Button_g", 0.9f);
        PlayerPrefs.SetFloat("Theme0Button_b", 0.9f);


        PlayerPrefs.SetFloat("Theme2Background_r", 0.35f);
        PlayerPrefs.SetFloat("Theme2Background_g", 0.35f);
        PlayerPrefs.SetFloat("Theme2Background_b", 0.35f);
        PlayerPrefs.SetFloat("Theme2Text_r", 1f);
        PlayerPrefs.SetFloat("Theme2Text_g", 0.6f);
        PlayerPrefs.SetFloat("Theme2Text_b", 0f);
        PlayerPrefs.SetFloat("Theme2Logo_r", 0f);
        PlayerPrefs.SetFloat("Theme2Logo_g", 0.3f);
        PlayerPrefs.SetFloat("Theme2Logo_b", 1f);
        PlayerPrefs.SetFloat("Theme2LogoO_r", 0.5f);
        PlayerPrefs.SetFloat("Theme2LogoO_g", 0.5f);
        PlayerPrefs.SetFloat("Theme2LogoO_b", 0.5f);
        PlayerPrefs.SetFloat("Theme2Checkmark_r", 0.55f);
        PlayerPrefs.SetFloat("Theme2Checkmark_g", 0.55f);
        PlayerPrefs.SetFloat("Theme2Checkmark_b", 0.55f);
        PlayerPrefs.SetFloat("Theme2RedButton_r", 1f);
        PlayerPrefs.SetFloat("Theme2RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme2RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme2Button_r", 0.5f);
        PlayerPrefs.SetFloat("Theme2Button_g", 0.5f);
        PlayerPrefs.SetFloat("Theme2Button_b", 0.5f);


        PlayerPrefs.SetFloat("Theme3Background_r", 1f);
        PlayerPrefs.SetFloat("Theme3Background_g", 1f);
        PlayerPrefs.SetFloat("Theme3Background_b", 1f);
        PlayerPrefs.SetFloat("Theme3Text_r", 0f);
        PlayerPrefs.SetFloat("Theme3Text_g", 0f);
        PlayerPrefs.SetFloat("Theme3Text_b", 0f);
        PlayerPrefs.SetFloat("Theme3Logo_r", 0f);
        PlayerPrefs.SetFloat("Theme3Logo_g", 0f);
        PlayerPrefs.SetFloat("Theme3Logo_b", 0f);
        PlayerPrefs.SetFloat("Theme3LogoO_r", 0f);
        PlayerPrefs.SetFloat("Theme3LogoO_g", 0f);
        PlayerPrefs.SetFloat("Theme3LogoO_b", 0f);
        PlayerPrefs.SetFloat("Theme3Checkmark_r", 1f);
        PlayerPrefs.SetFloat("Theme3Checkmark_g", 1f);
        PlayerPrefs.SetFloat("Theme3Checkmark_b", 1f);
        PlayerPrefs.SetFloat("Theme3RedButton_r", 1f);
        PlayerPrefs.SetFloat("Theme3RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme3RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme3Button_r", 1f);
        PlayerPrefs.SetFloat("Theme3Button_g", 1f);
        PlayerPrefs.SetFloat("Theme3Button_b", 1f);


        PlayerPrefs.SetFloat("Theme4Background_r", 0f);
        PlayerPrefs.SetFloat("Theme4Background_g", 0f);
        PlayerPrefs.SetFloat("Theme4Background_b", 0f);
        PlayerPrefs.SetFloat("Theme4Text_r", 0.45f);
        PlayerPrefs.SetFloat("Theme4Text_g", 0.45f);
        PlayerPrefs.SetFloat("Theme4Text_b", 0.45f);
        PlayerPrefs.SetFloat("Theme4Logo_r", 0.35f);
        PlayerPrefs.SetFloat("Theme4Logo_g", 0.35f);
        PlayerPrefs.SetFloat("Theme4Logo_b", 0.35f);
        PlayerPrefs.SetFloat("Theme4LogoO_r", 0.45f);
        PlayerPrefs.SetFloat("Theme4LogoO_g", 0.45f);
        PlayerPrefs.SetFloat("Theme4LogoO_b", 0.45f);
        PlayerPrefs.SetFloat("Theme4Checkmark_r", 0.5f);
        PlayerPrefs.SetFloat("Theme4Checkmark_g", 0.5f);
        PlayerPrefs.SetFloat("Theme4Checkmark_b", 0.5f);
        PlayerPrefs.SetFloat("Theme4RedButton_r", 0.5f);
        PlayerPrefs.SetFloat("Theme4RedButton_g", 0f);
        PlayerPrefs.SetFloat("Theme4RedButton_b", 0f);
        PlayerPrefs.SetFloat("Theme4Button_r", 0.1f);
        PlayerPrefs.SetFloat("Theme4Button_g", 0.1f);
        PlayerPrefs.SetFloat("Theme4Button_b", 0.1f);
        multiScriptHolder = new GameObject();
        multiScriptHolder.name = "MultiScriptHolder";
        multiScriptHolder.AddComponent<BluetoothReaderScript>();
        multiScriptHolder.AddComponent<BluetoothWriterScript>();
        multiScriptHolder.AddComponent<SwipeDetector>();
        multiScriptHolder.AddComponent<HM10Connect>();
        multiScriptHolder.tag = "MultiScriptHolder";
        DontDestroyOnLoad(multiScriptHolder);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Aveugle", -1) == -1 ? 21 : (PlayerPrefs.GetInt("Aveugle") == 0 ? 1 : 2));
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