using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThemeManagerScript : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] List<TextMeshProUGUI> basicText;
    [SerializeField] List<Image> buttons, redButttons, logos, logosO, clearer;
    [SerializeField] List<TMP_FontAsset> fontList;

    private void Awake()
    {
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/LiberationSans SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Roboto-Regular SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Bahnschrifft SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Verdana SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Autumn SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Univers SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/Comic-Sans-MS SDF"));
        fontList.Add(Resources.Load<TMP_FontAsset>("Fonts/DejaVuSans SDF"));
        
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

        foreach (Image x in clearer)
        {
            x.color = new Color(PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_r"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_g"), PlayerPrefs.GetFloat("Theme" + PlayerPrefs.GetInt("Theme") + "Checkmark_b"));
        }

        foreach(TextMeshProUGUI x in basicText)
        {
            x.font = fontList[PlayerPrefs.GetInt("Police")];
        }
    }
}
