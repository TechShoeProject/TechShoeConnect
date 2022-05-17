using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SonVibrationSaver : MonoBehaviour
{
    [SerializeField] TMP_Dropdown type;
    [SerializeField] Slider value;

    private void Awake()
    {
        type.value = PlayerPrefs.GetInt("VibrationSType", 0);
        value.value = PlayerPrefs.GetFloat("VibrationS", 0.5f);
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("VibrationS", value.value);
        PlayerPrefs.SetInt("VibrationSType", type.value);
    }
}
