using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThemeManagerScript : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] List<TextMeshProUGUI> basicText;
    [SerializeField] List<Image> buttons, redButttons, logos, logosO;

    private void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Camera.backgroundColor = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Background_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Background_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Background_b"));
        foreach(TextMeshProUGUI x in basicText)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Text_b"));
        }

        foreach(Image x in buttons)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Button_b"));
        }

        foreach (Image x in redButttons)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "RedButton_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "RedButton_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "RedButton_b"));
        }

        foreach (Image x in logos)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Logo_b"));
        }

        foreach (Image x in logosO)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "LogoO_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "LogoO_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "LogoO_b"));
        }
    }
}
