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
