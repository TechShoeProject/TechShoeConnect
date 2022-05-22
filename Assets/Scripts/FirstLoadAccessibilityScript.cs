using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadAccessibilityScript : MonoBehaviour
{
    GameObject holder;
    bool swippingUp, swippingDown;

    private void Awake()
    {
        holder = GameObject.FindGameObjectWithTag("MultiScriptHolder");
    }

    private void Update()
    {
        swippingDown = holder.GetComponent<SwipeDetector>().SwipeDown;
        swippingUp = holder.GetComponent<SwipeDetector>().SwipeUp;

        if (swippingUp)
        {
            PlayerPrefs.SetInt("Aveugle", 1);
            SceneManager.LoadScene(2);
        }else if (swippingDown)
        {
            PlayerPrefs.SetInt("Aveugle", 0);
            SceneManager.LoadScene(1);
        }
    }
}
